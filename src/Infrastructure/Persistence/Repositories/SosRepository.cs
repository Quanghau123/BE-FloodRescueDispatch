using Core.Application.Common.Paging;
using Core.Application.Interfaces.Persistence;
using Core.Application.Queries.Sos;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Infrastructure.Persistence.Repositories;

public sealed class SosRepository : ISosRepository
{
    private readonly AppDbContext _dbContext;
    private readonly GeometryFactory _geometryFactory;

    public SosRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
    }

    public Task<SosRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.SosRequests.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<SosRequest?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.SosRequests
            .Include(x => x.Citizen)
            .Include(x => x.Assignments)
            .ThenInclude(x => x.RescueTeam)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(SosRequest entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.SosRequests.AddAsync(entity, cancellationToken);
    }

    public async Task<PagedResult<SosRequest>> SearchAsync(SearchSosQuery query, CancellationToken cancellationToken = default)
    {
        var dbQuery = _dbContext.SosRequests
            .AsNoTracking()
            .Where(x => query.Status == null || x.Status == query.Status)
            .Where(x => query.CreatedFrom == null || x.CreatedAt >= query.CreatedFrom)
            .Where(x => query.CreatedTo == null || x.CreatedAt <= query.CreatedTo);

        dbQuery = query.SortBy switch
        {
            "created_at" => query.SortDirection == "asc"
                ? dbQuery.OrderBy(x => x.CreatedAt)
                : dbQuery.OrderByDescending(x => x.CreatedAt),

            "priority_score" => query.SortDirection == "asc"
                ? dbQuery.OrderBy(x => x.PriorityScore)
                : dbQuery.OrderByDescending(x => x.PriorityScore),

            _ => dbQuery.OrderByDescending(x => x.PriorityScore).ThenBy(x => x.CreatedAt)
        };

        var total = await dbQuery.LongCountAsync(cancellationToken);

        var items = await dbQuery
            .Skip(query.Skip)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<SosRequest>
        {
            Items = items,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalItems = total
        };
    }

    public async Task<IReadOnlyList<SosRequest>> GetMapItemsAsync(GetSosMapQuery query, CancellationToken cancellationToken = default)
    {
        var bbox = CreateBbox(query.MinLng, query.MinLat, query.MaxLng, query.MaxLat);

        return await _dbContext.SosRequests
            .AsNoTracking()
            .Where(x => query.Status == null || x.Status == query.Status)
            .Where(x => x.Location.Intersects(bbox))
            .OrderByDescending(x => x.PriorityScore)
            .Take(query.Zoom is <= 10 ? 500 : 2000)
            .ToListAsync(cancellationToken);
    }

    private Polygon CreateBbox(double minLng, double minLat, double maxLng, double maxLat)
    {
        var coordinates = new[]
        {
            new Coordinate(minLng, minLat),
            new Coordinate(maxLng, minLat),
            new Coordinate(maxLng, maxLat),
            new Coordinate(minLng, maxLat),
            new Coordinate(minLng, minLat)
        };

        return _geometryFactory.CreatePolygon(coordinates);
    }
}
