using Core.Application.Interfaces.PostGIS;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Notifications;

public interface INotificationService
{
    Task NotifyCitizenSosCreatedAsync(SosRequest sos, CancellationToken cancellationToken = default);

    Task NotifyDispatchSosCreatedAsync(SosRequest sos, CancellationToken cancellationToken = default);

    Task NotifyDispatchAssignmentChangedAsync(RescueAssignment assignment, CancellationToken cancellationToken = default);

    Task NotifyRescueTeamAssignedAsync(RescueAssignment assignment, CancellationToken cancellationToken = default);

    Task NotifyCitizenFloodAlertAsync(Guid userId, IReadOnlyList<FloodAlertResult> alerts, CancellationToken cancellationToken = default);
}

