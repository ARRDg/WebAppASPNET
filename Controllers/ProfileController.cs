using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppASPNET.Models;
using WebAppASPNET.Services.Implementations;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFriendshipService _friendshipService;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IUserService userService, IFriendshipService friendshipService, ILogger<ProfileController> logger)
        {
            _userService = userService;
            _friendshipService = friendshipService;
            _logger = logger;
        }

        [Route("[controller]/{id?}")]
        public async Task<IActionResult> Index(string? id)
        {
            string profileUserId = id ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(profileUserId))
            {
                return NotFound("Не удалось определить пользователя.");
            }

            ProfileModel? data = await _userService.GetProfileById(profileUserId);

            if (data == null)
            {
                return NotFound($"Профиль {profileUserId} не найден.");
            }

            FriendshipStatusInfo friendshipStatus = await _friendshipService.GetFriendshipStatus(profileUserId);

            ViewBag.FriendshipStatus = friendshipStatus;

            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == data.CurrentId)
            {
                return View(data);
            }
            else
            {
                return View("OtherProfile", data);
            }
        }
    }
}