using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IMaintenance_WorkDescriptionRepository
    {
        Task<IEnumerable<Maintenance_WorkDescription>> GetAllAsync();
        Task<Maintenance_WorkDescription?> GetByIdAsync(int id);
        Task<int> AddAsync(Maintenance_WorkDescription workDescription);
        Task<bool> UpdateAsync(Maintenance_WorkDescription workDescription);
        Task<bool> DeleteAsync(int id);

    }
}
