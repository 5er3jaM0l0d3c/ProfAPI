using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role> GetRoleByIdAsync(int id);
        Task<Role> CreateRoleAsync(CreateRoleRequest request);
        Task<Role> UpdateRoleAsync(UpdateRoleRequest request);
        Task<bool> DeleteRoleAsync(int id);
    }
}
