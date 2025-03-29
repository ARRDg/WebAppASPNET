namespace WebAppASPNET.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task SendFriendRequest();
        Task AcceptFriendRequest();
        Task DeclineFriendRequest();
        Task BlockUser();
    }
}
