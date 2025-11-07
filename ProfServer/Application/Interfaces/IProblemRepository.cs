using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IProblemRepository
    {
        Task<IEnumerable<Problem>> GetAllProblems();
        Task<Problem?> GetProblemById(int id);
        Task<int> CreateProblem(Problem problem);
        Task<bool> UpdateProblem(Problem problem);
        Task<bool> DeleteProblem(int id);
    }
}
