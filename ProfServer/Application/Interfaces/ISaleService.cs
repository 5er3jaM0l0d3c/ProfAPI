using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDTO>> GetSalesFromMachineAsync(int machineId);
        Task<IEnumerable<SaleDTO>> GetSalesWithProductAsync(int productId);
        Task<SaleDTO> GetSaleByIdAsync(int id);
        Task<SaleDTO> CreateSaleAsync(CreateSaleRequest request);
        //Task<SaleDTO> UpdateSaleAsync(UpdateSaleRequest request);
        Task<bool> DeleteSaleAsync(int id);
    }
}
