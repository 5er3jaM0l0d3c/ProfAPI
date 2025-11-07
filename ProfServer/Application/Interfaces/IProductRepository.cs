using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<int> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
