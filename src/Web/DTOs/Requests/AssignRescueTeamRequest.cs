namespace Web.DTOs.Requests;

public sealed class AssignRescueTeamRequest
{
    public Guid RescueTeamId { get; set; }

    public string? Note { get; set; }
}

