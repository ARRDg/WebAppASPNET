using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppASPNET.Models;

namespace WebAppASPNET.Controllers
{
    public static class ControllerExtensions
    {
        public static ProfileModel GetMyProfile(this Controller c)
        {
            return new ProfileModel()
            {
                CurrentId = c.User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                CurrentName = c.User.FindFirstValue(ClaimTypes.Name)!,
                CurrentEmail = c.User.FindFirstValue(ClaimTypes.Email)!
            };
        }
    }
}
