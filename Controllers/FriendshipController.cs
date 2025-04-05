using Microsoft.AspNetCore.Mvc;
using WebAppASPNET.Models;
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

        [HttpGet]
        public async Task<IActionResult> SendFriendRequest(string id)
        {
            await _friendshipService.SendFriendRequest(id);
            return RedirectToAction("Index", "Profile", new { Id = id });
        }

        [HttpGet]
        public async Task<IActionResult> AcceptFriendRequest(string id)
        {
            await _friendshipService.AcceptFriendRequest(id);
            return RedirectToAction("Index", "Profile", new { Id = id });
        }

        [HttpGet]
        public async Task<IActionResult> DeclineFriendRequest(string id)
        {
            await _friendshipService.DeclineFriendRequest(id);
            return RedirectToAction("Index", "Profile", new { Id = id });
        }

        [HttpGet]
        public async Task<IActionResult> BlockUser()
        {
            await _friendshipService.BlockUser();
            return View();
        }

    }
}