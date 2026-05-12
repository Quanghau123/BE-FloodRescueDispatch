using Core.Domain.Common;
using Core.Domain.Enums;
using NetTopologySuite.Geometries;

namespace Core.Domain.Entities;

public sealed class SosRequest : SoftDeletableEntity
{
    public Guid CitizenId { get; set; }

    public AppUser? Citizen { get; set; }

    public Point Location { get; set; } = default!;

    public string? AddressText { get; set; }

    public string? Description { get; set; }

    public int PeopleCount { get; set; } = 1;

    public bool HasInjuredPeople { get; set; }

    public bool HasChildren { get; set; }

    public bool HasElderly { get; set; }

    public SosStatus Status { get; set; } = SosStatus.Pending;

    public int PriorityScore { get; set; }

    public DateTimeOffset? AssignedAt { get; set; }

    public DateTimeOffset? ResolvedAt { get; set; }

    public ICollection<RescueAssignment> Assignments { get; set; } = new List<RescueAssignment>();
}