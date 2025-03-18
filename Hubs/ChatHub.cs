using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WebAppASPNET.Hubs
{
    public class ChatHub: Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
        public async Task JoinRoom(string roomId)
        {
            var userName = Context.User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                userName = "Anonymous";
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("UserJoined", $"{userName} вошел в чат!");
        }

        public async Task LeaveRoom(string roomId)
        {
            var userName = Context.User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                userName = "Anonymous";
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("UserLeave", $"{userName} вошел в чат!");
        }

        public async Task SendMessage(string roomId, string message)
        {
            var userName = Context.User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                userName = "Anonymous";
            }
            await Clients.Group(roomId).SendAsync("ReceiveMessage", userName, message);
        }
    }
}
