using Core.Domain.Enums;

namespace Web.DTOs.Requests;

public sealed class UpdateFloodZoneRequest
{
    public string Name { get; set; } = string.Empty;

    public FloodSeverity Severity { get; set; }

    public FloodZoneStatus Status { get; set; }

    public string? WktPolygon { get; set; }

    public string? Description { get; set; }
}

