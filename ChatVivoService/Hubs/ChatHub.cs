using Microsoft.AspNetCore.SignalR;

namespace ChatVivoService.Hubs;

public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
