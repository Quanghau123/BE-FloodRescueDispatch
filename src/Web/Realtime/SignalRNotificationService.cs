using Core.Application.Interfaces.Notifications;
using Core.Application.Interfaces.PostGIS;
using Core.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Web.Realtime;

public sealed class SignalRNotificationService : INotificationService
{
    private readonly IHubContext<DispatchHub> _dispatchHub;
    private readonly IHubContext<CitizenAlertHub> _citizenAlertHub;
    private readonly IHubContext<RescueTeamHub> _rescueTeamHub;

    public SignalRNotificationService(
        IHubContext<DispatchHub> dispatchHub,
        IHubContext<CitizenAlertHub> citizenAlertHub,
        IHubContext<RescueTeamHub> rescueTeamHub)
    {
        _dispatchHub = dispatchHub;
        _citizenAlertHub = citizenAlertHub;
        _rescueTeamHub = rescueTeamHub;
    }

    public Task NotifyCitizenSosCreatedAsync(SosRequest sos, CancellationToken cancellationToken = default)
    {
        return _citizenAlertHub.Clients
            .Group($"user:{sos.CitizenId}")
            .SendAsync("sos_created", new
            {
                sos.Id,
                sos.Status,
                sos.PriorityScore,
                sos.CreatedAt
            }, cancellationToken);
    }

    public Task NotifyDispatchSosCreatedAsync(SosRequest sos, CancellationToken cancellationToken = default)
    {
        return _dispatchHub.Clients
            .Group("dispatch")
            .SendAsync("sos_created", new
            {
                sos.Id,
                sos.CitizenId,
                Longitude = sos.Location.X,
                Latitude = sos.Location.Y,
                sos.Status,
                sos.PriorityScore,
                sos.CreatedAt
            }, cancellationToken);
    }

    public Task NotifyDispatchAssignmentChangedAsync(RescueAssignment assignment, CancellationToken cancellationToken = default)
    {
        return _dispatchHub.Clients
            .Group("dispatch")
            .SendAsync("assignment_changed", new
            {
                assignment.Id,
                assignment.SosRequestId,
                assignment.RescueTeamId,
                assignment.Status,
                assignment.AssignedAt
            }, cancellationToken);
    }

    public Task NotifyRescueTeamAssignedAsync(RescueAssignment assignment, CancellationToken cancellationToken = default)
    {
        return _rescueTeamHub.Clients
            .Group($"team:{assignment.RescueTeamId}")
            .SendAsync("team_assigned", new
            {
                assignment.Id,
                assignment.SosRequestId,
                assignment.Status,
                assignment.AssignedAt,
                assignment.Note
            }, cancellationToken);
    }

    public Task NotifyCitizenFloodAlertAsync(Guid userId, IReadOnlyList<FloodAlertResult> alerts, CancellationToken cancellationToken = default)
    {
        return _citizenAlertHub.Clients
            .Group($"user:{userId}")
            .SendAsync("flood_alert", alerts, cancellationToken);
    }
}

