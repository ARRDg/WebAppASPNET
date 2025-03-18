using Microsoft.EntityFrameworkCore;
using WebAppASPNET.Data;
using WebAppASPNET.Models;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly DataContext _context;

        public RoomService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateRoom(RoomModel model)
        {
            string roomId = Guid.NewGuid().ToString("N").Substring(0, 16);
            var room = new Room
            {
                RoomId = roomId,
                Name = model.Name
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return room.RoomId;
        }

        public async Task<RoomModel?> GetRoomModel(string roomId)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);

            if (room == null)
            {
                return null;
            }

            return new RoomModel
            {
                Id = room.Id,
                RoomId = room.RoomId,
                Name = room.Name
            };
        }
    }
}