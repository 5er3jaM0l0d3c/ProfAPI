using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsInMachineAsync(int machineId);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(CreateProductRequest request);
        Task<Product> UpdateProductAsync(UpdateProductRequest request);
        Task<bool> DeleteProductAsync(int id);
    }
}
