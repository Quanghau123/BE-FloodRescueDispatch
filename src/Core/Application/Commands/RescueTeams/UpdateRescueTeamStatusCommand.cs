using Core.Domain.Enums;

namespace Core.Application.Commands.RescueTeams;

public sealed class UpdateRescueTeamStatusCommand
{
    public Guid RescueTeamId { get; set; }

    public RescueTeamStatus Status { get; set; }
}