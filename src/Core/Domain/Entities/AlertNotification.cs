using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

public sealed class AlertNotification : AuditableEntity
{
    public Guid UserId { get; set; }

    public AppUser? User { get; set; }

    public Guid? FloodZoneId { get; set; }

    public FloodZone? FloodZone { get; set; }

    public FloodSeverity Severity { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTimeOffset? ReadAt { get; set; }
}