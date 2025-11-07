using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IWorkDescriptionService
    {
        Task<IEnumerable<WorkDescription>> GetAllWorkDescriptions();
        Task<WorkDescription> GetWorkDescriptionById(int id);
        Task<WorkDescription> CreateWorkDescription(CreateWorkDescriptionRequest request);
        Task<WorkDescription> UpdateWorkDescription(UpdateWorkDescriptionRequest request);
        Task<bool> DeleteWorkDescription(int id);
    }
}
