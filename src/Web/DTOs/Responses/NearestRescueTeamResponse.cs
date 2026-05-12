using Core.Domain.Enums;

namespace Web.DTOs.Responses;

public sealed class NearestRescueTeamResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public RescueTeamStatus Status { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public double DistanceMeters { get; set; }
}

