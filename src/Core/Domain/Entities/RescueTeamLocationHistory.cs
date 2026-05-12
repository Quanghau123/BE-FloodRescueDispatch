using Core.Domain.Common;
using NetTopologySuite.Geometries;

namespace Core.Domain.Entities;

public sealed class RescueTeamLocationHistory : AuditableEntity
{
    public Guid RescueTeamId { get; set; }

    public RescueTeam? RescueTeam { get; set; }

    public Point Location { get; set; } = default!;

    public DateTimeOffset CapturedAt { get; set; } = DateTimeOffset.UtcNow;
}