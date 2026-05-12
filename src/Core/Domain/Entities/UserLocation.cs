using NetTopologySuite.Geometries;
using Core.Domain.Common;

namespace Core.Domain.Entities;

public sealed class UserLocation : AuditableEntity
{
    public Guid UserId { get; set; }

    public AppUser User { get; set; } = null!;

    public Point Location { get; set; } = default!;

    public double AccuracyMeters { get; set; }

    public DateTimeOffset CapturedAt { get; set; } = DateTimeOffset.UtcNow;
}