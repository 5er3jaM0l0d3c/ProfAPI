using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMaintenanceRepository
    {
        Task<IEnumerable<Maintenance>> GetMaintenancesByMachineId(int machineId);
        Task<IEnumerable<Maintenance>> GetMaintenancesByUserId(int userId);
        Task<Maintenance?> GetMaintenanceById(int id);
        Task<int> CreateMaintanance(Maintenance maintenance);
        Task<bool> UpdateMaintenance(Maintenance maintenance);
        Task<bool> DeleteMaintenance(int id);
    }
}
