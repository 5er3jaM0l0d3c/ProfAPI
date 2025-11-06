using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ILogger<ManufacturerService> _logger;  

        public ManufacturerService(IManufacturerRepository manufacturerRepository, ILogger<ManufacturerService> logger)
        {
            _manufacturerRepository = manufacturerRepository;
            _logger = logger;
        }

        public async Task<Manufacturer> AddManufacturerAsync(CreateManufacturerRequest request)
        {
            try
            {
                Manufacturer manufacturer = new()
                {
                    Name = request.Name
                };

                var manufacturerId = await _manufacturerRepository.AddManufacturerAsync(manufacturer);
                return await GetManufacturerByIdAsync(manufacturerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding manufacturer");
                throw;
            }
        }

        public async Task<bool> DeleteManufacturerAsync(int id)
        {
            try
            {
                await GetManufacturerByIdAsync(id); // Ensure manufacturer exists

                return await _manufacturerRepository.DeleteManufacturerAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting manufacturer");
                throw;
            }
        }

        public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync()
        {
            try
            {
                return await _manufacturerRepository.GetAllManufacturersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all manufacturers");
                throw;
            }
        }

        public async Task<Manufacturer> GetManufacturerByIdAsync(int id)
        {
            try
            {
                var manufacturer = await _manufacturerRepository.GetManufacturerByIdAsync(id);

                if(manufacturer == null)
                    throw new NotFoundException(nameof(Manufacturer), id);

                return manufacturer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving manufacturer");
                throw;
            }
        }

        public async Task<Manufacturer> UpdateManufacturerAsync(UpdateManufacturerRequest request)
        {
            try
            {
                await GetManufacturerByIdAsync(request.Id); // Ensure manufacturer exists

                Manufacturer manufacturer = new()
                {
                    Id = request.Id,
                    Name = request.Name
                };

                if(!await _manufacturerRepository.UpdateManufacturerAsync(manufacturer))
                    throw new ConflictException("Failed to update manufacturer");

                return await GetManufacturerByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating manufacturer");
                throw;
            }
        }
    }
}
