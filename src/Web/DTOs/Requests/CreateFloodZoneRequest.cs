using Core.Domain.Enums;

namespace Web.DTOs.Requests;

public sealed class CreateFloodZoneRequest
{
    public string Name { get; set; } = string.Empty;

    public FloodSeverity Severity { get; set; }

    public string WktPolygon { get; set; } = string.Empty;

    public string? Description { get; set; }
}

