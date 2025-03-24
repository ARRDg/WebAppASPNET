using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppASPNET.Data;
using WebAppASPNET.Models;
using WebAppASPNET.Services.Implementations;
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
        [Route("[controller]/{id?}")]
        public IActionResult Index(string? id)
        {
            ProfileModel data;

            if (string.IsNullOrEmpty(id))
            {
                data = this.GetMyProfile();
            }
            else
            {
                data = this.GetMyProfile();
            }

            return View();
        }

/*        public async Task<IActionResult> Index()
        {
*//*            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }


            if (*//*roomModel*//* == null)
            {
                return RedirectToAction("Index", "Home");
            }*//*

            return View(*//*roomModel*//*);
        }*/
    }
}