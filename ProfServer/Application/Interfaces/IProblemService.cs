using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IProblemService
    {
        Task<IEnumerable<Problem>> GetAllProblems();
        Task<Problem> GetProblemById(int id);
        Task<Problem> CreateProblem(CreateProblemRequest request);
        Task<Problem> UpdateProblem(UpdateProblemRequest request);
        Task<bool> DeleteProblem(int id);
    }
}
