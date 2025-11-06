using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IManufactureCountryRepository
    {
        Task<IEnumerable<ManufactureCountry>> GetManufactureCountries();
        Task<ManufactureCountry?> GetManufactureCountryById(int id);
        Task<int> AddManufactureCountry(ManufactureCountry manufactureCountry);
        Task<bool> UpdateManufactureCountry(ManufactureCountry manufactureCountry);
        Task<bool> DeleteManufactureCountry(int id);
    }
}
