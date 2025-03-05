using Microsoft.AspNetCore.Mvc;
using WebAppASPNET.Data;
using WebAppASPNET.Models;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            return  RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (await _userService.EmailExistsAsync(model.Email))
            {
                ModelState.AddModelError("Email", "An account with this Email already exists");
            }

            if (!ModelState.IsValid)
                return View(model);

            await _userService.CreateUserAsync(model);
            return RedirectToAction("Index", "Home");
        }
    }
}