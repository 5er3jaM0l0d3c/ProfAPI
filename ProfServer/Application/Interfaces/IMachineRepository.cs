using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;
using System.Data;

namespace ProfServer.Application.Interfaces
{
    public interface IMachineRepository
    {
        Task<IEnumerable<Machine>> GetMachinesByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<Machine>> GetMachinesAsync();
        Task<Machine?> GetMachineByIdAsync(int id);
        Task<int> AddMachineAsync(Machine machine);
        Task<bool> UpdateMachineAsync(Machine machine);
        Task<bool> DeleteMachineAsync(int id);

    }
}
