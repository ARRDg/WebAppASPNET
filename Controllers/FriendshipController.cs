using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Controllers
{
    [Authorize]
    public class FriendshipController : Controller
    {
        private readonly IFriendshipService _friendshipService;
        private readonly ILogger<FriendshipController> _logger;

        public FriendshipController(IFriendshipService friendshipService, ILogger<FriendshipController> logger)
        {
            _friendshipService = friendshipService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(string id)
        {
            await _friendshipService.SendFriendRequest(id);
            return RedirectToAction("Index", "Profile", new { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> AcceptFriendRequest(string id)
        {
            await _friendshipService.AcceptFriendRequest(id);
            return RedirectToAction("Index", "Profile", new { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> DeclineFriendRequest(string id)
        {
            await _friendshipService.DeclineFriendRequest(id);
            return RedirectToAction("Index", "Profile", new { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFriend(string id)
        {
            await _friendshipService.RemoveFriend(id);
            return RedirectToAction("Index", "Profile", new { Id = id });
        }
    }
}