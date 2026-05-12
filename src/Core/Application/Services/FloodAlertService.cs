using Core.Application.Interfaces.Notifications;
using Core.Application.Interfaces.PostGIS;
using Core.Application.Queries.Geo;

namespace Core.Application.Services;

public sealed class FloodAlertService
{
    private readonly IGeoQueryService _geoQueryService;
    private readonly INotificationService _notificationService;

    public FloodAlertService(IGeoQueryService geoQueryService, INotificationService notificationService)
    {
        _geoQueryService = geoQueryService;
        _notificationService = notificationService;
    }

    public async Task<IReadOnlyList<FloodAlertResult>> CheckAlertAsync(
        CheckFloodAlertQuery query,
        CancellationToken cancellationToken = default)
    {
        var alerts = await _geoQueryService.CheckPointInFloodZonesAsync(
            query.Longitude,
            query.Latitude,
            cancellationToken);

        if (alerts.Count > 0)
            await _notificationService.NotifyCitizenFloodAlertAsync(query.UserId, alerts, cancellationToken);

        return alerts;
    }
}

