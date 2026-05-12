using Core.Domain.Enums;

namespace Web.DTOs.Requests;

public sealed class UpdateRescueTeamStatusRequest
{
    public RescueTeamStatus Status { get; set; }
}

