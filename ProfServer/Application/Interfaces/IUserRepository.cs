using ProfServer.Application.DTOs;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<int> UserExistsAsync(string login, byte[] password);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<int> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
