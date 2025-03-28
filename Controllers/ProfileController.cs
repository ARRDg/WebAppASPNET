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
            public async Task<ViewResult> Index(string? id)
            {
                ProfileModel data;

                if (string.IsNullOrEmpty(id) || User.FindFirstValue(ClaimTypes.NameIdentifier) == id)
                {
                    data = this.GetMyProfile();
                }
                else
                {
                    data = await _userService.GetProfileById(id);
                }

                if (data == null)
                {
                    return View("Error");
                }

                return View(data);
            }
    }
}