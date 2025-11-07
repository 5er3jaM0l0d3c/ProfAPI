using ProfServer.Models;

namespace ProfServer.Application.DTOs
{
    public class MaintenanceDTO
    {
        public int Id { get; set; }
        public required MachineDTO Machine { get; set; }
        public DateOnly Date { get; set; }
        public required UserDTO User { get; set; }
        public IEnumerable<Problem> Problems { get; set; } = null!;
        public IEnumerable<WorkDescription> WorkDescriptiones { get; set; } = null!;
    }
}