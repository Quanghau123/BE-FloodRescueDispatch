namespace Core.Application.Interfaces.PostGIS;

public interface IGeoQueryService
{
    Task<IReadOnlyList<FloodAlertResult>> CheckPointInFloodZonesAsync(
        double longitude,
        double latitude,
        CancellationToken cancellationToken = default);

    Task<NearestShelterResult?> FindNearestShelterAsync(
        double longitude,
        double latitude,
        double radiusMeters,
        CancellationToken cancellationToken = default);

    Task<NearestRescueTeamResult?> FindNearestRescueTeamAsync(
        double longitude,
        double latitude,
        double radiusMeters,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Guid>> FindUsersInsideFloodZoneAsync(
        Guid floodZoneId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SosRegionStatisticResult>> CountSosByFloodZoneAsync(
        DateTimeOffset? from,
        DateTimeOffset? to,
        CancellationToken cancellationToken = default);
}

