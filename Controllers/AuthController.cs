using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.Authenticate(model.Email, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (await _userService.EmailExists(model.Email))
            {
                ModelState.AddModelError("Email", "Пользователь с этим email уже существует");
            }

            if (!ModelState.IsValid)
                return View(model);

            await _userService.CreateUser(model);
            return RedirectToAction("Index", "Home");
        }
    }
}