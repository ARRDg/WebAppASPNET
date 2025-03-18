using Microsoft.AspNetCore.SignalR;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IUserService _userService;

        public ChatHub(IUserService userService)
        {
            _userService = userService;
        }

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
            var userName = _userService.GetNameUser(Context.User);

            if (string.IsNullOrEmpty(userName))
            {
                userName = "Anonymous";
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("UserJoined", $"{userName} вошел в чат!");
        }

        public async Task LeaveRoom(string roomId)
        {
            var userName = _userService.GetNameUser(Context.User);

            if (string.IsNullOrEmpty(userName))
            {
                userName = "Anonymous";
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("UserLeave", $"{userName} вошел в чат!");
        }

        public async Task SendMessage(string roomId, string message)
        {
            var userName = _userService.GetNameUser(Context.User);

            if (string.IsNullOrEmpty(userName))
            {
                userName = "Anonymous";
            }
            await Clients.Group(roomId).SendAsync("ReceiveMessage", userName, message);
        }
    }
}
