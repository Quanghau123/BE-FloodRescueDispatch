using Core.Application.Interfaces.Persistence;
using Core.Application.Queries.FloodZones;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Infrastructure.Persistence.Repositories;

public sealed class FloodZoneRepository : IFloodZoneRepository
{
    private readonly AppDbContext _dbContext;
    private readonly GeometryFactory _geometryFactory;

    public FloodZoneRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
    }

    public Task<FloodZone?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.FloodZones.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(FloodZone entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.FloodZones.AddAsync(entity, cancellationToken);
    }

    public async Task<IReadOnlyList<FloodZone>> GetMapItemsAsync(GetFloodZonesMapQuery query, CancellationToken cancellationToken = default)
    {
        var bbox = CreateBbox(query.MinLng, query.MinLat, query.MaxLng, query.MaxLat);

        return await _dbContext.FloodZones
            .AsNoTracking()
            .Where(x => query.Status == null || x.Status == query.Status)
            .Where(x => query.Severity == null || x.Severity == query.Severity)
            .Where(x => x.Boundary.Intersects(bbox))
            .OrderByDescending(x => x.Severity)
            .Take(query.Zoom is <= 10 ? 300 : 1000)
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
