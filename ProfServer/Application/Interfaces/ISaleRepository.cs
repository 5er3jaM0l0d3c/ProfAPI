using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetSalesFromMachineAsync(int machineId);
        Task<IEnumerable<Sale>> GetSalesFromProductAsync(int productId);
        Task<Sale?> GetSaleByIdAsync(int id);
        Task<Sale?> GetSaleByProductAndMachineIdAsync(int productId, int machineId);
        Task<int> CreateSaleAsync(Sale sale);
        Task<bool> UpdateSaleAsync(Sale sale);
        Task<bool> DeleteSaleAsync(int id);
    }
}
