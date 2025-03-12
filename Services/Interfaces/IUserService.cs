using WebAppASPNET.Data;
using WebAppASPNET.Models;

namespace WebAppASPNET.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        Task<bool> EmailExists(string email);
        Task CreateUser(RegisterModel model);
    }
}