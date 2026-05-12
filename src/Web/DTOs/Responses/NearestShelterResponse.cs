namespace Web.DTOs.Responses;

public sealed class NearestShelterResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public int AvailableSlots { get; set; }

    public double DistanceMeters { get; set; }
}

