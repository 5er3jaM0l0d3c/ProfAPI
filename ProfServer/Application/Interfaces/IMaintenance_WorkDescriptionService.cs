using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMaintenance_WorkDescriptionService
    {
        Task<IEnumerable<Maintenance_WorkDescription>> GetAllAsync();
        Task<Maintenance_WorkDescription> GetByIdAsync(int id);
        Task<Maintenance_WorkDescription> AddAsync(CreateMaintenance_WorkDescriptionRequest request);
        Task<Maintenance_WorkDescription> UpdateAsync(UpdateMaintenance_WorkDescriptionRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
