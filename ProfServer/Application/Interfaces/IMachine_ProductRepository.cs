using ProfServer.Application.DTOs;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMachine_ProductRepository
    {
        Task<IEnumerable<int>> GetProductsInMachineAsync(int machineId);
        Task<IEnumerable<int>> GetMachinesWhereProductAsync(int productId);
        Task<Machine_Product?> GetMachine_ProductByIdAsync(int id);
        Task<Machine_Product?> GetMachine_ProductByMachineAndProductId(int machineId, int productId);
        Task<int> CreateMachine_ProductAsync(Machine_Product machine_Product);
        Task<bool> UpdateMachine_ProductAsync(Machine_Product machine_Product);
        Task<bool> DeleteMachine_ProductAsync(int id);
    }
}
