using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;
using System.Reflection.PortableExecutable;

namespace ProfServer.Application.Services
{
    public class ManufactureCountryService : IManufactureCountryService
    {
        private readonly IManufactureCountryRepository _manufactureCountryRepository;
        private readonly ILogger<ManufactureCountryService> _logger;

        public ManufactureCountryService(IManufactureCountryRepository manufactureCountryRepository, ILogger<ManufactureCountryService> logger)
        {
            _manufactureCountryRepository = manufactureCountryRepository;
            _logger = logger;
        }

        public async Task<ManufactureCountry> CreateManufactureCountryAsync(CreateManufactureCountryRequest request)
        {
            try
            {
                ManufactureCountry manufactureCountry = new ManufactureCountry
                {
                    Name = request.Name
                };

                var id = await _manufactureCountryRepository.AddManufactureCountry(manufactureCountry);

                return await GetManufactureCountryByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating ManufactureCountry");
                throw;
            }
        }

        public async Task<bool> DeleteManufactureCountryAsync(int id)
        {
            try
            {
                await GetManufactureCountryByIdAsync(id); // Ensure it exists

                return await _manufactureCountryRepository.DeleteManufactureCountry(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting ManufactureCountry");
                throw;
            }
        }

        public async Task<IEnumerable<ManufactureCountry>> GetManufactureCountriesAsync()
        {
            try
            {
                return await _manufactureCountryRepository.GetManufactureCountries();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving ManufactureCountries");
                throw;
            }
        }

        public async Task<ManufactureCountry> GetManufactureCountryByIdAsync(int id)
        {
            try
            {
                var manufactureCountry = await _manufactureCountryRepository.GetManufactureCountryById(id);

                if (manufactureCountry == null)
                    throw new NotFoundException(nameof(ManufactureCountry), id);

                return manufactureCountry;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving ManufactureCountry");
                throw;
            }
        }

        public async Task<ManufactureCountry> UpdateManufactureCountryAsync(UpdateManufactureCountryRequest request)
        {
            try
            {
                await GetManufactureCountryByIdAsync(request.Id);

                ManufactureCountry manufactureCountry = new ManufactureCountry
                {
                    Id = request.Id,
                    Name = request.Name
                };

                if (!await _manufactureCountryRepository.UpdateManufactureCountry(manufactureCountry))
                    throw new ConflictException($"ManufactureCountry with Id {manufactureCountry.Id} could not be updated");

                return await GetManufactureCountryByIdAsync(manufactureCountry.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating ManufactureCountry");
                throw;
            }
        }
    }
}
