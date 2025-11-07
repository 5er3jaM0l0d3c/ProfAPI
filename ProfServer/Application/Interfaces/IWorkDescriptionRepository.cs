using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IWorkDescriptionRepository
    {
        Task<IEnumerable<WorkDescription>> GetAllWorkDescriptions();
        Task<WorkDescription?> GetWorkDescriptionById(int id);
        Task<int> CreateWorkDescription(WorkDescription workDescription);
        Task<bool> UpdateWorkDescription(WorkDescription workDescription);
        Task<bool> DeleteWorkDescription(int id);
    }
}
