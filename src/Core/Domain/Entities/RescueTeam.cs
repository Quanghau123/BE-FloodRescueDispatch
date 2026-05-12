using Core.Domain.Common;
using Core.Domain.Enums;
using NetTopologySuite.Geometries;

namespace Core.Domain.Entities;

public sealed class RescueTeam : SoftDeletableEntity
{
    public string Name { get; set; } = string.Empty;

    public string? LeaderPhone { get; set; }

    public RescueTeamStatus Status { get; set; } = RescueTeamStatus.Available;

    public Point? CurrentLocation { get; set; }

    public DateTimeOffset? LastLocationUpdatedAt { get; set; }

    public int Capacity { get; set; }

    public ICollection<RescueTeamLocationHistory> LocationHistories { get; set; } = new List<RescueTeamLocationHistory>();

    public ICollection<RescueAssignment> Assignments { get; set; } = new List<RescueAssignment>();
}