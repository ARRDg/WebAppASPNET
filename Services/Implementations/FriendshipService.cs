using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAppASPNET.Data;
using WebAppASPNET.Services.Interfaces;
using static WebAppASPNET.Data.Friendship;

namespace WebAppASPNET.Services.Implementations
{
    public class FriendshipService : IFriendshipService
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

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                _logger.LogError("Не удалось получить ID текущего пользователя.");
                throw new InvalidOperationException("Не удалось определить текущего пользователя.");
            }
            return userId;
        }

        private async Task<Friendship?> FindFriendshipAsync(int userId1, int userId2)
        {
            return await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.RequesterId == userId1 && f.ReceiverId == userId2) ||
                    (f.RequesterId == userId2 && f.ReceiverId == userId1));
        }

        public async Task SendFriendRequest(string profileUserIdStr)
        {
            if (!int.TryParse(profileUserIdStr, out int profileUserId))
            {
                _logger.LogError("Неверный формат ID пользователя профиля");
                return;
            }

            int currentUserId = GetCurrentUserId();

            if (currentUserId == profileUserId)
            {
                _logger.LogWarning("Попытка отправить запрос в друзья самому себе");
                return;
            }

            var existingFriendship = await FindFriendshipAsync(currentUserId, profileUserId);
            if (existingFriendship != null)
            {
                _logger.LogWarning("Отношения уже существуют");
                return;
            }

            var friendship = new Friendship
            {
                RequesterId = currentUserId,
                ReceiverId = profileUserId,
                Status = FriendshipStatus.Pending,
                CreatesAt = DateTime.UtcNow
            };

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task AcceptFriendRequest(string requesterUserIdStr)
        {
            if (!int.TryParse(requesterUserIdStr, out int requesterUserId))
            {
                _logger.LogError($"Неверный формат ID отправителя запроса");
                return;
            }

            int currentUserId = GetCurrentUserId();

            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.RequesterId == requesterUserId && f.ReceiverId == currentUserId && f.Status == FriendshipStatus.Pending);

            if (friendship == null)
            {
                _logger.LogWarning($"Не найден запрос");
                return;
            }

            friendship.Status = FriendshipStatus.Accepted; 
            await _context.SaveChangesAsync();
        }

        public async Task DeclineFriendRequest(string requesterUserIdStr)
        {
            if (!int.TryParse(requesterUserIdStr, out int requesterUserId))
            {
                _logger.LogError($"Неверный формат ID отправителя запроса");
                return;
            }

            int currentUserId = GetCurrentUserId();

            _logger.LogInformation($"Попытка отклонить запрос от {requesterUserId} пользователем {currentUserId}.");

            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.RequesterId == requesterUserId && f.ReceiverId == currentUserId && f.Status == FriendshipStatus.Pending);

            if (friendship == null)
            {
                _logger.LogWarning($"Не найден запрос");
                return;
            }

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFriend(string friendUserIdStr)
        {
            if (!int.TryParse(friendUserIdStr, out int friendUserId))
            {
                _logger.LogError($"Неверный формат ID друга для удаления");
                return;
            }

            int currentUserId = GetCurrentUserId();

            var friendship = await FindFriendshipAsync(currentUserId, friendUserId);

            if (friendship == null || friendship.Status != FriendshipStatus.Accepted)
            {
                _logger.LogWarning($"Не найдена дружба");
                return;
            }

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task<FriendshipStatusInfo> GetFriendshipStatus(string profileUserIdStr)
        {
            if (!int.TryParse(profileUserIdStr, out int profileUserId))
            {
                _logger.LogWarning($"Не удалось распознать ID профиля: {profileUserIdStr}");
                return FriendshipStatusInfo.Error;
            }

            int currentUserId;
            try
            {
                currentUserId = GetCurrentUserId();
            }
            catch (InvalidOperationException)
            {
                return FriendshipStatusInfo.None;
            }


            if (currentUserId == profileUserId)
            {
                return FriendshipStatusInfo.Self;
            }

            var friendship = await FindFriendshipAsync(currentUserId, profileUserId);

            if (friendship == null)
            {
                return FriendshipStatusInfo.None;
            }

            if (friendship.Status == FriendshipStatus.Accepted)
            {
                return FriendshipStatusInfo.Friends;
            }

            if (friendship.Status == FriendshipStatus.Pending)
            {
                if (friendship.RequesterId == currentUserId)
                {
                    return FriendshipStatusInfo.RequestSent;
                }
                else
                {
                    return FriendshipStatusInfo.RequestReceived;
                }
            }

            return FriendshipStatusInfo.None;
        }
    }

    public enum FriendshipStatusInfo
    {
        None,
        Friends,
        RequestSent,
        RequestReceived,
        Self,
        Error
    }
}