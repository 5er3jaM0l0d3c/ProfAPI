using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMaintenance_ProblemService
    {
        Task<IEnumerable<Maintenance_Problem>> GetAllProblemsAsync();
        Task<Maintenance_Problem> GetProblemByIdAsync(int id);
        Task<Maintenance_Problem> CreateProblemAsync(CreateMaintenance_ProblemRequest request);
        Task<Maintenance_Problem> UpdateProblemAsync(UpdateMaintenance_ProblemRequest request);
        Task<bool> DeleteProblemAsync(int id);
    }
}
