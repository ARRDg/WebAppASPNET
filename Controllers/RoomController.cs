using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppASPNET.Data;
using WebAppASPNET.Models;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly DataContext _context;
        private readonly IRoomService _roomService;

        public RoomController(DataContext context, IRoomService roomService)
        {
            _context = context;
            _roomService = roomService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomModel model)
        {
            var roomId = await _roomService.GenerateRoom(model);

            return RedirectToAction("Index", new { v = roomId });
        }

        public async Task<IActionResult> Index(string v)
        {
            if (string.IsNullOrEmpty(v))
            {
                return RedirectToAction("Create");
            }

            var roomModel = await _roomService.GetRoomModel(v);

            if (roomModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(roomModel);
        }
    }
}