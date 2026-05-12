using Microsoft.AspNetCore.SignalR;

namespace Web.Realtime;

public sealed class RescueTeamHub : Hub
{
    public Task JoinTeamRoom(Guid teamId)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, $"team:{teamId}");
    }
}

