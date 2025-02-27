using Microsoft.AspNetCore.Mvc;
using WebAppASPNET.Data;
using WebAppASPNET.Models;

namespace WebAppASPNET.Controllers
{
    public class AuthController : Controller
    {
        private DataContext dataContext;
        private ILogger<AuthController> logger;
        public AuthController(DataContext dataContext, ILogger<AuthController> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
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
        public IActionResult Register(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            dataContext.Add(model);
            dataContext.SaveChanges();
            logger.LogInformation(model.Id.ToString());

            return RedirectToAction("Index", "Home");
        }
    }
}