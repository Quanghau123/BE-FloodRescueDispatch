using Microsoft.AspNetCore.SignalR;

namespace Web.Realtime;

public sealed class CitizenAlertHub : Hub
{
    public Task JoinUserRoom(Guid userId)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userId}");
    }
}

