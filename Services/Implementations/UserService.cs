using Microsoft.EntityFrameworkCore;
using WebAppASPNET.Data;
using WebAppASPNET.Models;
using WebAppASPNET.Services.Interfaces;

namespace WebAppASPNET.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(RegisterModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                AgreeToTerms = model.AgreeToTerms
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}