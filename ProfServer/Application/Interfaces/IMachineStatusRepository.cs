using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMachineStatusRepository
    {
        Task<IEnumerable<MachineStatus>> GetMachineStatusesAsync();
        Task<MachineStatus?> GetMachineStatusByIdAsync(int id);
        Task<int> AddMachineStatusAsync(MachineStatus status);
        Task<bool> UpdateMachineStatusAsync(MachineStatus status);
        Task<bool> DeleteMachineStatusAsync(int id);
    }
}
