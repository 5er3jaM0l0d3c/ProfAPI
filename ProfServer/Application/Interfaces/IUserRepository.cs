using ProfServer.Application.DTOs;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<int> UserExistsAsync(string login, byte[] password);
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int id);
        Task<int> CreateUserAsync(User userDto);
        Task<bool> UpdateUserAsync(User userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
