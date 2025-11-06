using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IManufactureCountryService
    {
        Task<IEnumerable<ManufactureCountry>> GetManufactureCountriesAsync();
        Task<ManufactureCountry> GetManufactureCountryByIdAsync(int id);
        Task<ManufactureCountry> CreateManufactureCountryAsync(CreateManufactureCountryRequest request);
        Task<ManufactureCountry> UpdateManufactureCountryAsync(UpdateManufactureCountryRequest request);
        Task<bool> DeleteManufactureCountryAsync(int id);
    }
}
