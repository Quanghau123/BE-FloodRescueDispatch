namespace Core.Application.Commands.RescueTeams;

public sealed class UpdateRescueTeamLocationCommand
{
    public Guid RescueTeamId { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }
}

