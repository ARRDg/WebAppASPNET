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
        public IActionResult SendFriendRequest()
        {
            return View();
        }

        public IActionResult AcceptFriendRequest()
        {
            return View();
        }
    }
}