using Core.Domain.Enums;

namespace Core.Application.Interfaces.PostGIS;

public sealed class NearestShelterResult
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public int AvailableSlots { get; set; }

    public double DistanceMeters { get; set; }
}

public sealed class NearestRescueTeamResult
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public RescueTeamStatus Status { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public double DistanceMeters { get; set; }
}

public sealed class FloodAlertResult
{
    public Guid FloodZoneId { get; set; }

    public string FloodZoneName { get; set; } = string.Empty;

    public FloodSeverity Severity { get; set; }

    public string Message { get; set; } = string.Empty;
}

public sealed class SosRegionStatisticResult
{
    public Guid FloodZoneId { get; set; }

    public string FloodZoneName { get; set; } = string.Empty;

    public long SosCount { get; set; }
}

