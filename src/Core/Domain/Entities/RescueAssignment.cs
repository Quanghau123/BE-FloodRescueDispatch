using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

public sealed class RescueAssignment : AuditableEntity
{
    public Guid SosRequestId { get; set; }

    public SosRequest? SosRequest { get; set; }

    public Guid RescueTeamId { get; set; }

    public RescueTeam? RescueTeam { get; set; }

    public AssignmentStatus Status { get; set; } = AssignmentStatus.Assigned;

    public DateTimeOffset AssignedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? AcceptedAt { get; set; }

    public DateTimeOffset? ArrivedAt { get; set; }

    public DateTimeOffset? CompletedAt { get; set; }

    public string? Note { get; set; }
}