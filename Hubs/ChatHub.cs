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

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            // Дастаю данные из бд для Подключенных
            // Отправляю метод LeaveRoom
            return base.OnDisconnectedAsync(exception);
        }
        public async Task JoinRoom(string roomId)
        {
            // Тут буду принимать Context.ConnectionId и roomId.
            // Сохраняю все в бд для Подключенный
            var userName = _userService.GetNameUser(Context.User);

            if (string.IsNullOrEmpty(userName))
            {
                userName = "Anonymous";
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("UserJoined", $"{userName} вошел в чат!");
        }

        public async Task LeaveRoom(string userName, string roomId)
        {
            await Clients.Group(roomId).SendAsync("UserLeave", $"{userName} покинул чат!");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
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
