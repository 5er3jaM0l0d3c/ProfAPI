using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync();
        Task<Manufacturer?> GetManufacturerByIdAsync(int id);
        Task<int> AddManufacturerAsync(Manufacturer manufacturer);
        Task<bool> UpdateManufacturerAsync(Manufacturer manufacturer);
        Task<bool> DeleteManufacturerAsync(int id);
    }
}
