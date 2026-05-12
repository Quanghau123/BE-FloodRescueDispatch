using Core.Domain.Enums;

namespace Core.Application.Commands.FloodZones;

public sealed class CreateFloodZoneCommand
{
    public string Name { get; set; } = string.Empty;

    public FloodSeverity Severity { get; set; }

    public string WktPolygon { get; set; } = string.Empty;

    public string? Description { get; set; }
}