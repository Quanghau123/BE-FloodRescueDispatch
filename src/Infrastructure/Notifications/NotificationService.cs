using Core.Application.Interfaces.Notifications;
using Core.Application.Interfaces.PostGIS;
using Core.Domain.Entities;

namespace Infrastructure.Notifications;

public sealed class NotificationService : INotificationService
{
    public Task NotifyCitizenSosCreatedAsync(SosRequest sos, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task NotifyDispatchSosCreatedAsync(SosRequest sos, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task NotifyDispatchAssignmentChangedAsync(RescueAssignment assignment, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task NotifyRescueTeamAssignedAsync(RescueAssignment assignment, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task NotifyCitizenFloodAlertAsync(Guid userId, IReadOnlyList<FloodAlertResult> alerts, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}

