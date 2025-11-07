using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly ILogger<ProblemService> _logger;

        public ProblemService(IProblemRepository problemRepository, ILogger<ProblemService> logger)
        {
            _problemRepository = problemRepository;
            _logger = logger;
        }

        public async Task<Problem> CreateProblem(CreateProblemRequest request)
        {
            try
            {
                Problem problem = new Problem() { Name = request.Name };

                var createdId = await _problemRepository.CreateProblem(problem);

                return await GetProblemById(createdId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating problem");
                throw;
            }
        }

        public async Task<bool> DeleteProblem(int id)
        {
            try
            {
                await GetProblemById(id); // Ensure problem exists
                return await _problemRepository.DeleteProblem(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting problem");
                throw;
            }
        }

        public async Task<IEnumerable<Problem>> GetAllProblems()
        {
            try
            {
                return await _problemRepository.GetAllProblems();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all problems");
                throw;
            }
        }

        public async Task<Problem> GetProblemById(int id)
        {
            try
            {
                var problem = await _problemRepository.GetProblemById(id);
                if (problem == null)
                    throw new NotFoundException(nameof(Problem), id);

                return problem;
            }
            catch (Exception)
            {
                _logger.LogError("Error retrieving problem with id {ProblemId}", id);
                throw;
            }
        }

        public async Task<Problem> UpdateProblem(UpdateProblemRequest request)
        {
            try
            {
                await GetProblemById(request.Id); // Ensure problem exists

                Problem problem = new Problem() { Id = request.Id, Name = request.Name };

                if(!await _problemRepository.UpdateProblem(problem))
                    throw new ConflictException("Problem could not be updated");

                return await GetProblemById(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating problem with id {ProblemId}", request.Id);
                throw;
            }
        }
    }
}
