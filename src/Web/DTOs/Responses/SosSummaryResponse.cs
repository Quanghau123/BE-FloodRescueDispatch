using Core.Domain.Enums;

namespace Web.DTOs.Responses;

public sealed class SosSummaryResponse
{
    public Guid Id { get; set; }

    public Guid CitizenId { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public SosStatus Status { get; set; }

    public int PriorityScore { get; set; }

    public int PeopleCount { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

