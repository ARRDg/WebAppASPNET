using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppASPNET.Models;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

            [Route("[controller]/{id?}")]
            public async Task<IActionResult> Index(string? id)
            {
                ProfileModel data = await _userService.GetProfileById(id ?? User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (data == null)
            {
                return NotFound();
            }

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) == data.CurrentId)
                {
                    return View(data);
                }
            
                return View("OtherProfile", data);
        }
    }
}