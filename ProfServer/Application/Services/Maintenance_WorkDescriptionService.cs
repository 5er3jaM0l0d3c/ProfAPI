using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class Maintenance_WorkDescriptionService : IMaintenance_WorkDescriptionService
    {
        private readonly IMaintenance_WorkDescriptionRepository _maintenance_WorkDescriptionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<Maintenance_WorkDescriptionService> _logger;

        public Maintenance_WorkDescriptionService(
            IMaintenance_WorkDescriptionRepository repository,
            IMapper mapper,
            ILogger<Maintenance_WorkDescriptionService> logger)
        {
            _maintenance_WorkDescriptionRepository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maintenance_WorkDescription> AddAsync(CreateMaintenance_WorkDescriptionRequest request)
        {
            try
            {
                var maintenance_workDescription = _mapper.Map<Maintenance_WorkDescription>(request);

                var id = await _maintenance_WorkDescriptionRepository.AddAsync(maintenance_workDescription);

                return await GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding Maintenance_WorkDescription");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await GetByIdAsync(id); // Ensure it exists

                return await _maintenance_WorkDescriptionRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Maintenance_WorkDescription");
                throw;
            }
        }

        public async Task<IEnumerable<Maintenance_WorkDescription>> GetAllAsync()
        {
            try
            {
                var maintenance_workDescriptions = await _maintenance_WorkDescriptionRepository.GetAllAsync();

                return maintenance_workDescriptions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all Maintenance_WorkDescriptions");
                throw;
            }
        }

        public async Task<Maintenance_WorkDescription> GetByIdAsync(int id)
        {
            try
            {
                var maintenance = await _maintenance_WorkDescriptionRepository.GetByIdAsync(id);
                if (maintenance == null)
                    throw new NotFoundException(nameof(Maintenance_WorkDescription), id);

                return maintenance;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Maintenance_WorkDescription with ID {Id}", id);
                throw;
            }
        }

        public async Task<Maintenance_WorkDescription> UpdateAsync(UpdateMaintenance_WorkDescriptionRequest request)
        {
            try
            {
                await GetByIdAsync(request.Id); // Ensure it exists

                var maintenance_workDescription = _mapper.Map<Maintenance_WorkDescription>(request);

                if(!await _maintenance_WorkDescriptionRepository.UpdateAsync(maintenance_workDescription))
                    throw new ConflictException("Failed to update Maintenance_WorkDescription");

                return await GetByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating Maintenance_WorkDescription with ID {Id}", request.Id);
                throw;
            }
        }
    }
}
