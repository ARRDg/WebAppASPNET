using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppASPNET.Data;

namespace WebAppASPNET.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly DataContext _context;

        public RoomController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roomName)
        {
            string roomId = Guid.NewGuid().ToString("N").Substring(0, 16);
            var room = new Room { RoomId = roomId, Name = roomName };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { v = roomId });
        }

        public async Task<IActionResult> Index(string v)
        {
            if (string.IsNullOrEmpty(v))
            {
                return RedirectToAction("Create");
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == v);

            if (room == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RoomId = v;
            return View();
        }
    }
}
