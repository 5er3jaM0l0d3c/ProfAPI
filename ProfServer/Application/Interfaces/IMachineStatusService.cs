using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMachineStatusService
    {
        Task<IEnumerable<MachineStatus>> GetMachineStatusesAsync();
        Task<MachineStatus> GetMachineStatusByIdAsync(int id);
        Task<MachineStatus> CreateMachineStatusAsync(CreateMachineStatusRequest request);
        Task<MachineStatus> UpdateMachineStatusAsync(UpdateMachineStatusRequest request);
        Task<bool> DeleteMachineStatusAsync(int id);
    }
}
