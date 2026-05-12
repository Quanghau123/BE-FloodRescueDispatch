namespace Core.Application.Queries.Geo;

public sealed class CheckFloodAlertQuery
{
    public Guid UserId { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }
}

