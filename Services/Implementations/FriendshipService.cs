using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAppASPNET.Data;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Services.Implementations
{
    public class FriendshipService: IFriendshipService
    {
        private readonly DataContext _context;
        private readonly ILogger<FriendshipService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FriendshipService(DataContext context, ILogger<FriendshipService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendFriendRequest(string id)
        {
            _logger.LogInformation(id);

            var friendship = new Friendship
            {
                RequesterId = int.Parse(id),
                ReceiverId = int.Parse(s: _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Status = Friendship.FriendshipStatus.Pending,
                CreatesAt = DateTime.Now
            };
            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();
        }
        public async Task AcceptFriendRequest(string id)
        {
            Friendship friendship = await GetFriendship(id);

            if (friendship == null)
                return;

            friendship.Status = Friendship.FriendshipStatus.Accepted;
            await _context.SaveChangesAsync();
        }
        public async Task DeclineFriendRequest(string id)
        {
            Friendship friendship = await GetFriendship(id);

            if (friendship == null)
                return;

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task BlockUser()
        {

        }

        public async Task<Friendship> GetFriendship(string id)
        {
            int RequesterId = int.Parse(id);
            int ReceiverId = int.Parse(s: _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            Friendship friendship = await _context.Friendships.Where(u => u.RequesterId == RequesterId && u.ReceiverId == ReceiverId).FirstOrDefaultAsync();

            return friendship;
        }

    }
}
