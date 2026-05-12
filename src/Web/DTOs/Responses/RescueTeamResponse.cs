using Core.Domain.Enums;

namespace Web.DTOs.Responses;

public sealed class RescueTeamResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? LeaderPhone { get; set; }

    public RescueTeamStatus Status { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public DateTimeOffset? LastLocationUpdatedAt { get; set; }

    public int Capacity { get; set; }
}

