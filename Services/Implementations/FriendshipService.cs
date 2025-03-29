using WebAppASPNET.Data;

namespace WebAppASPNET.Services.Implementations
{
    public class FriendshipService
    {
        private readonly DataContext _context;

        public FriendshipService(DataContext context)
        {
            _context = context;
        }

        public async Task SendFriendRequest()
        {

        }
        public async Task AcceptFriendRequest()
        {

        }
    }
}
