using Core.Domain.Enums;

namespace Web.DTOs.Responses;

public sealed class SosDetailResponse
{
    public Guid Id { get; set; }

    public Guid CitizenId { get; set; }

    public string? CitizenName { get; set; }

    public string? CitizenPhone { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string? AddressText { get; set; }

    public string? Description { get; set; }

    public int PeopleCount { get; set; }

    public bool HasInjuredPeople { get; set; }

    public bool HasChildren { get; set; }

    public bool HasElderly { get; set; }

    public SosStatus Status { get; set; }

    public int PriorityScore { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public List<RescueAssignmentResponse> Assignments { get; set; } = new();
}

public sealed class RescueAssignmentResponse
{
    public Guid Id { get; set; }

    public Guid RescueTeamId { get; set; }

    public string? RescueTeamName { get; set; }

    public AssignmentStatus Status { get; set; }

    public DateTimeOffset AssignedAt { get; set; }

    public string? Note { get; set; }
}

