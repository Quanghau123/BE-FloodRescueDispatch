using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Domain.Factories;

public static class RescueAssignmentFactory
{
    public static RescueAssignment Create(Guid sosRequestId, Guid rescueTeamId, string? note = null)
    {
        return new RescueAssignment
        {
            SosRequestId = sosRequestId,
            RescueTeamId = rescueTeamId,
            Status = AssignmentStatus.Assigned,
            AssignedAt = DateTimeOffset.UtcNow,
            Note = note
        };
    }
}