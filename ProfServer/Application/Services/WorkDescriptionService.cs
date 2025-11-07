using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class WorkDescriptionService : IWorkDescriptionService
    {
        private readonly IWorkDescriptionRepository _workDescriptionRepository;
        private readonly ILogger<WorkDescriptionService> _logger;
        public WorkDescriptionService(IWorkDescriptionRepository workDescriptionRepository,
            ILogger<WorkDescriptionService> logger)
        {
            _workDescriptionRepository = workDescriptionRepository;
            _logger = logger;
        }

        public async Task<WorkDescription> CreateWorkDescription(CreateWorkDescriptionRequest request)
        {
            try
            {
                WorkDescription workDescription = new() { Name = request.Name };

                var workDescriptionId = await _workDescriptionRepository.CreateWorkDescription(workDescription);

                return await GetWorkDescriptionById(workDescriptionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating work description");
                throw;
            }
        }

        public async Task<bool> DeleteWorkDescription(int id)
        {
            try
            {
                await GetWorkDescriptionById(id);
                return await _workDescriptionRepository.DeleteWorkDescription(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting work description");
                throw;
            }
        }


        public async Task<IEnumerable<WorkDescription>> GetAllWorkDescriptions()
        {
            try
            {
                return await _workDescriptionRepository.GetAllWorkDescriptions();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all work descriptions");
                throw;
            }
        }

        public async Task<WorkDescription> GetWorkDescriptionById(int id)
        {
            try
            {
                var workDescription = await _workDescriptionRepository.GetWorkDescriptionById(id);
                if (workDescription == null)
                    throw new NotFoundException(nameof(WorkDescription), id);

                return workDescription;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting work description by id {Id}", id);
                throw;
            }
        }

        public async Task<WorkDescription> UpdateWorkDescription(UpdateWorkDescriptionRequest request)
        {
            try
            {
                await GetWorkDescriptionById(request.Id);

                WorkDescription workDescription = new()
                {
                    Id = request.Id,
                    Name = request.Name
                };

                if (!await _workDescriptionRepository.UpdateWorkDescription(workDescription))
                    throw new ConflictException("Failed to update work description");

                return await GetWorkDescriptionById(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating work description with id {Id}", request.Id);
                throw;
            }
        }
    }
}
