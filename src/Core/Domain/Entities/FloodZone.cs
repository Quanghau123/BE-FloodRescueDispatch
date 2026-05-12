using Core.Domain.Common;
using Core.Domain.Enums;
using NetTopologySuite.Geometries;

namespace Core.Domain.Entities;

public sealed class FloodZone : SoftDeletableEntity
{
    public string Name { get; set; } = string.Empty;

    public FloodSeverity Severity { get; set; }

    public FloodZoneStatus Status { get; set; } = FloodZoneStatus.Active;

    public Polygon Boundary { get; set; } = default!;

    public string? Description { get; set; }

    public DateTimeOffset EffectiveFrom { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? EffectiveTo { get; set; }
}