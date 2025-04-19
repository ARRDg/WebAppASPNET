using WebAppASPNET.Services.Implementations;

namespace WebAppASPNET.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task SendFriendRequest(string profileUserId);
        Task AcceptFriendRequest(string requesterUserId);
        Task DeclineFriendRequest(string requesterUserId);
        Task RemoveFriend(string friendUserId);
        Task<FriendshipStatusInfo> GetFriendshipStatus(string profileUserId);
    }
}