using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMaintenance_ProblemRepository
    {
        Task<IEnumerable<Maintenance_Problem>> GetAllAsync();
        Task<Maintenance_Problem?> GetByIdAsync(int id);
        Task<int> AddAsync(Maintenance_Problem problem);
        Task<bool> UpdateAsync(Maintenance_Problem problem);
        Task<bool> DeleteAsync(int id);
    }
}
