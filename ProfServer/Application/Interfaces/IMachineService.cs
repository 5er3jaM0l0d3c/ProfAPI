using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineDTO>> GetMachinesWhereProductAsync(int productId);
        Task<IEnumerable<MachineDTO>> GetMachinesAsync();
        Task<MachineDTO> GetMachineByIdAsync(int id);
        Task<MachineDTO> CreateMachineAsync(CreateMachineRequest machine);
        Task<MachineDTO> UpdateMachineAsync(UpdateMachineRequest machine);
        Task<bool> DeleteMachineAsync(int id);
    }
}
