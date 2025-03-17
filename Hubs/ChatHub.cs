using Microsoft.AspNetCore.SignalR;

namespace WebAppASPNET.Hubs
{
    public interface IChatClient
    {
        public Task ServerMassage(string username, string message);
        public Task SendClientMessage(string roomId, string user, string message);
    }
    public class ChatHub : Hub<IChatClient>
    {
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).ServerMassage("UserJoined", $"{Context.ConnectionId} присоеденился к чату!");
        }

        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).ServerMassage("UserLeft", $"{Context.ConnectionId} покинул чат!");
        }

        public async Task SendMessage(string roomId, string user, string message)
        {
            await Clients.Group(roomId).SendClientMessage("ReceiveMessage", user, message);
        }
    }
}
