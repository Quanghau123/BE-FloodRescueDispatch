using Microsoft.AspNetCore.SignalR;

namespace Web.Realtime;

public sealed class DispatchHub : Hub
{
    public Task JoinDispatchRoom()
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, "dispatch");
    }
}

