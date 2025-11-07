using ProfServer.Application.DTOs;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMachine_ProductService
    {
        Task<Machine_Product> GetMachine_ProductByIdAsync(int id);
        Task<Machine_Product> GetMachine_ProductByMachineAndProductAsync(int machineId, int productId);
        Task<Machine_Product> CreateMachine_ProductAsync(CreateMachine_ProductRequest request);
        Task<Machine_Product> UpdateMachine_ProductAsync(UpdateMachine_ProductRequest request);
        Task<bool> DeleteMachine_ProductAsync(int id);
    }
}
