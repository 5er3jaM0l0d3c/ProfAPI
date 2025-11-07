using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class Maintenance_ProblemService : IMaintenance_ProblemService
    {
        private readonly IMaintenance_ProblemRepository _maintenanceProblemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<Maintenance_ProblemService> _logger;
        public Maintenance_ProblemService(IMaintenance_ProblemRepository maintenanceProblemRepository, ILogger<Maintenance_ProblemService> logger, IMapper mapper)
        {
            _maintenanceProblemRepository = maintenanceProblemRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Maintenance_Problem> CreateProblemAsync(CreateMaintenance_ProblemRequest request)
        {
            try
            {
                Maintenance_Problem maintenance_Problem = new()
                {
                    MaintenanceId = request.MaintenanceId,
                    ProblemId = request.ProblemId
                };

                var maintenanceProblemId = await _maintenanceProblemRepository.AddAsync(maintenance_Problem);

                return await GetProblemByIdAsync(maintenanceProblemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Maintenance_Problem");
                throw;
            }
        }

        public async Task<bool> DeleteProblemAsync(int id)
        {
            try
            {
                await GetProblemByIdAsync(id);

                return await _maintenanceProblemRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Maintenance_Problem");
                throw;
            }
        }

        public async Task<IEnumerable<Maintenance_Problem>> GetAllProblemsAsync()
        {
            try
            {
                var maintenance_problems = await _maintenanceProblemRepository.GetAllAsync();

                return maintenance_problems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all Maintenance_Problems");
                throw;
            }
        }

        public async Task<Maintenance_Problem> GetProblemByIdAsync(int id)
        {
            try
            {
                var maintenance_problem = await _maintenanceProblemRepository.GetByIdAsync(id);
                if (maintenance_problem == null)
                    throw new NotFoundException(nameof(Maintenance_Problem), id);

                return maintenance_problem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Maintenance_Problem by Id");
                throw;
            }
        }

        public async Task<Maintenance_Problem> UpdateProblemAsync(UpdateMaintenance_ProblemRequest request)
        {
            try
            {
                await GetProblemByIdAsync(request.Id);

                Maintenance_Problem maintenance_Problem = new()
                {
                    Id = request.Id,
                    MaintenanceId = request.MaintenanceId,
                    ProblemId = request.ProblemId
                };

                if(!await _maintenanceProblemRepository.UpdateAsync(maintenance_Problem))
                    throw new ConflictException("Failed to update Maintenance_Problem");

                return await GetProblemByIdAsync(maintenance_Problem.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Maintenance_Problem");
                throw;
            }
        }
    }
}
