namespace Core.Application.Commands.Dispatch;

public sealed class AssignRescueTeamCommand
{
    public Guid SosRequestId { get; set; }

    public Guid RescueTeamId { get; set; }

    public string? Note { get; set; }
}