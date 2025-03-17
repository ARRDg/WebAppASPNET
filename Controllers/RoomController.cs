using Microsoft.AspNetCore.Mvc;

namespace WebAppASPNET.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
