namespace Core.Application.Queries.Geo;

public sealed class FindNearestRescueTeamQuery
{
    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public double RadiusMeters { get; set; } = 20_000;
}

