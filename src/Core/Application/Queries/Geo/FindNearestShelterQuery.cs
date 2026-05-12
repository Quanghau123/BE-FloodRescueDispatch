namespace Core.Application.Queries.Geo;

public sealed class FindNearestShelterQuery
{
    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public double RadiusMeters { get; set; } = 10_000;
}

