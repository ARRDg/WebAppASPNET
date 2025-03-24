using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;
using WebAppASPNET.Data;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly DataContext _context;
        private readonly HttpClient _httpClient;

        public ProfileController(IUserService userService, DataContext context, IHttpClientFactory f)
        {
            _userService = userService;
            _context = context;

            _httpClient = f.CreateClient("api1");
        }

        public async Task<IActionResult> Index()
        {
            var r = await _httpClient.GetAsync("?name=Tom");
            var c = await r.Content.ReadFromJsonAsync<Root>();


            ViewBag.C = c;

            return View(this.GetProfile());
        }

        public class Root
        {
            [JsonPropertyName("count")]
            public int? Count { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("age")]
            public int? Age { get; set; }
        }

    }
}