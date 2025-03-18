using WebAppASPNET.Models;

namespace WebAppASPNET.Services.Interfaces
{
    public interface IRoomService
    {
        Task<string> GenerateRoom(RoomModel model);
        Task<RoomModel?> GetRoomModel(string roomId);
    }
}