using WebAppASPNET.Models;

namespace WebAppASPNET.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task SendFriendRequest(string id);
        Task AcceptFriendRequest(string id);
        Task DeclineFriendRequest(string id);
        Task BlockUser();
    }
}
