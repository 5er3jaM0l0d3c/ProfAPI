using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;

namespace ProfServer.Application.Interfaces
{
    public interface IMaintenanceService
    {
        Task<IEnumerable<MaintenanceDTO>> GetMaintenancesByMachineId(int machineId);
        Task<IEnumerable<MaintenanceDTO>> GetMaintenancesByUserId(int userId);
        Task<MaintenanceDTO> GetMaintenanceById(int id);
        Task<MaintenanceDTO> CreateMaintenance(CreateMaintenanceRequest request);
        Task<MaintenanceDTO> UpdateMaintenance(UpdateMaintenanceRequest request);
        Task<bool> DeleteMaintenance(int id);
    }
}
