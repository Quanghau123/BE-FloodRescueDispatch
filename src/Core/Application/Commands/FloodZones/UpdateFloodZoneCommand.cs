using Core.Domain.Enums;

namespace Core.Application.Commands.FloodZones;

public sealed class UpdateFloodZoneCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public FloodSeverity Severity { get; set; }

    public FloodZoneStatus Status { get; set; }

    public string? WktPolygon { get; set; }

    public string? Description { get; set; }
}