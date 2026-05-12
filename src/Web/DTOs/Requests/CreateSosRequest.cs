namespace Web.DTOs.Requests;

public sealed class CreateSosRequest
{
    public Guid CitizenId { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string? AddressText { get; set; }

    public string? Description { get; set; }

    public int PeopleCount { get; set; } = 1;

    public bool HasInjuredPeople { get; set; }

    public bool HasChildren { get; set; }

    public bool HasElderly { get; set; }
}

