using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppASPNET.Data;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly DataContext _context;

        public ProfileController(IUserService userService, DataContext context)
        {
            _userService = userService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}