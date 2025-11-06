using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IManufacturerService
    {
        Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync();
        Task<Manufacturer> GetManufacturerByIdAsync(int id);
        Task<Manufacturer> AddManufacturerAsync(CreateManufacturerRequest request);
        Task<Manufacturer> UpdateManufacturerAsync(UpdateManufacturerRequest request);
        Task<bool> DeleteManufacturerAsync(int id);
    }
}
