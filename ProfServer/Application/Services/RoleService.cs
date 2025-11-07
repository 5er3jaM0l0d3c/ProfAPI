using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;
using System.Net.WebSockets;

namespace ProfServer.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<Role> CreateRoleAsync(CreateRoleRequest request)
        {
            try
            {
                Role role = new()
                {
                    Name = request.Name 
                };
                var roleId = await _roleRepository.CreateRoleAsync(role);

                return await GetRoleByIdAsync(roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role");
                throw;
            }
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            try
            {
                await GetRoleByIdAsync(id);

                return await _roleRepository.DeleteRoleAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting role");
                throw;
            }
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            try
            {
                var role = await _roleRepository.GetRoleByIdAsync(id);
                if(role == null)
                    throw new NotFoundException(nameof(Role), id);

                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving role");
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            try
            {
                return await _roleRepository.GetRolesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles");
                throw;
            }
        }

        public async Task<Role> UpdateRoleAsync(UpdateRoleRequest request)
        {
            try
            {
                await GetRoleByIdAsync(request.Id);

                Role role = new()
                {
                    Id = request.Id,
                    Name = request.Name
                };

                if(!await _roleRepository.UpdateRoleAsync(role))
                    throw new ConflictException("Failed to update role");
                
                return await GetRoleByIdAsync(role.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating role");
                throw;
            }
        }
    }
}
