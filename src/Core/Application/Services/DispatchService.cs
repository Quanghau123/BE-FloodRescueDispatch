using Core.Application.Commands.Dispatch;
using Core.Application.Interfaces.Notifications;
using Core.Application.Interfaces.Persistence;
using Core.Domain.Enums;
using Core.Domain.Factories;

namespace Core.Application.Services;

public sealed class DispatchService
{
    private readonly ISosRepository _sosRepository;
    private readonly IRescueTeamRepository _rescueTeamRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;

    public DispatchService(
        ISosRepository sosRepository,
        IRescueTeamRepository rescueTeamRepository,
        IUnitOfWork unitOfWork,
        INotificationService notificationService)
    {
        _sosRepository = sosRepository;
        _rescueTeamRepository = rescueTeamRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task AssignAsync(AssignRescueTeamCommand command, CancellationToken cancellationToken = default)
    {
        var sos = await _sosRepository.GetDetailAsync(command.SosRequestId, cancellationToken)
            ?? throw new InvalidOperationException("SOS request not found.");

        var team = await _rescueTeamRepository.GetByIdAsync(command.RescueTeamId, cancellationToken)
            ?? throw new InvalidOperationException("Rescue team not found.");

        if (sos.Status is SosStatus.Resolved or SosStatus.Cancelled)
            throw new InvalidOperationException("Cannot assign resolved or cancelled SOS.");

        if (team.Status != RescueTeamStatus.Available)
            throw new InvalidOperationException("Rescue team is not available.");

        var assignment = RescueAssignmentFactory.Create(sos.Id, team.Id, command.Note);

        sos.Assignments.Add(assignment);
        sos.Status = SosStatus.Assigned;
        sos.AssignedAt = DateTimeOffset.UtcNow;
        sos.UpdatedAt = DateTimeOffset.UtcNow;

        team.Status = RescueTeamStatus.Busy;
        team.UpdatedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.NotifyDispatchAssignmentChangedAsync(assignment, cancellationToken);
        await _notificationService.NotifyRescueTeamAssignedAsync(assignment, cancellationToken);
    }
}

