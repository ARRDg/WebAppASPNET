using WebAppASPNET.Models;

namespace WebAppASPNET.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> EmailExistsAsync(string email);
        Task CreateUserAsync(RegisterModel model);
    }
}