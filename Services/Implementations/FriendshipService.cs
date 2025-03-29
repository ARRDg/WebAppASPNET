using WebAppASPNET.Data;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Services.Implementations
{
    public class FriendshipService: IFriendshipService
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
        public async Task DeclineFriendRequest()
        {

        }

        public async Task BlockUser()
        {

        }
    }
}
