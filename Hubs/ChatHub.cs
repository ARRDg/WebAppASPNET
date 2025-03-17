using Microsoft.AspNetCore.SignalR;

namespace WebAppASPNET.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await SendMessageToAll("Message");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessageToOther(string message)
        {
            await Clients.All.SendAsync("RecieveMessageToAll", $"{Context.ConnectionId}: {message} ");
        }
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("RecieveMessageToAll", $"{message} {Context.ConnectionId}");
        }
    }
}
