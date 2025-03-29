using Microsoft.AspNetCore.Mvc;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Controllers
{
    public class FriendshipController : Controller
    {
        private IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }
        public async Task<IActionResult> SendFriendRequest()
        {
            await _friendshipService.SendFriendRequest();
            return View();
        }

        public async Task<IActionResult> AcceptFriendRequest()
        {
            await _friendshipService.AcceptFriendRequest();
            return View();
        }

        public async Task<IActionResult> DeclineFriendRequest()
        {
            await _friendshipService.DeclineFriendRequest();
            return View();
        }
        public async Task<IActionResult> BlockUser()
        {
            await _friendshipService.BlockUser();
            return View();
        }

    }
}