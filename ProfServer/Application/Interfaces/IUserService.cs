using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.DTOs.Responses;

namespace ProfServer.Application.Interfaces
{
    public interface IUserService
    {
        Task<TokenResponse> Authenticate(AuthRequest request); 
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(CreateUserRequest request);
        Task<UserDTO> UpdateUserAsync(UpdateUserRequest request);
        Task<bool> DeleteUserAsync(int id);
    }
}
