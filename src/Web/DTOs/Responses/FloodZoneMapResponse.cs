using Core.Domain.Enums;

namespace Web.DTOs.Responses;

public sealed class FloodZoneMapResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public FloodSeverity Severity { get; set; }

    public FloodZoneStatus Status { get; set; }

    public string WktBoundary { get; set; } = string.Empty;
}

