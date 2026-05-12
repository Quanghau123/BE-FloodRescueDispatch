using Core.Domain.Enums;

namespace Web.DTOs.Responses;

public sealed class AlertResponse
{
    public Guid FloodZoneId { get; set; }

    public string FloodZoneName { get; set; } = string.Empty;

    public FloodSeverity Severity { get; set; }

    public string Message { get; set; } = string.Empty;
}

