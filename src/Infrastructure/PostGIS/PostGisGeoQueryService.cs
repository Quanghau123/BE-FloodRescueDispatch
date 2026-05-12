using Core.Application.Interfaces.PostGIS;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.PostGIS;

public sealed class PostGisGeoQueryService : IGeoQueryService
{
    private readonly AppDbContext _dbContext;

    public PostGisGeoQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<FloodAlertResult>> CheckPointInFloodZonesAsync(
        double longitude,
        double latitude,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database
            .SqlQueryRaw<FloodAlertResult>(PostGisSql.PointInFloodZone, longitude, latitude)
            .ToListAsync(cancellationToken);
    }

    public async Task<NearestShelterResult?> FindNearestShelterAsync(
        double longitude,
        double latitude,
        double radiusMeters,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database
            .SqlQueryRaw<NearestShelterResult>(PostGisSql.NearestShelter, longitude, latitude, radiusMeters)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<NearestRescueTeamResult?> FindNearestRescueTeamAsync(
        double longitude,
        double latitude,
        double radiusMeters,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database
            .SqlQueryRaw<NearestRescueTeamResult>(PostGisSql.NearestRescueTeam, longitude, latitude, radiusMeters)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> FindUsersInsideFloodZoneAsync(
        Guid floodZoneId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database
            .SqlQueryRaw<Guid>(PostGisSql.UsersInsideFloodZone, floodZoneId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SosRegionStatisticResult>> CountSosByFloodZoneAsync(
        DateTimeOffset? from,
        DateTimeOffset? to,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database
            .SqlQueryRaw<SosRegionStatisticResult>(
                PostGisSql.SosCountByFloodZone,
                from ?? (object)DBNull.Value,
                to ?? (object)DBNull.Value)
            .ToListAsync(cancellationToken);
    }
}
