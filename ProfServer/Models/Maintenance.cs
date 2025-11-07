using ProfServer.Application.DTOs;

namespace ProfServer.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public MachineDTO Machine { get; set; } = null!;
        public DateOnly Date { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; } = null!;
        public required IEnumerable<int> ProblemsIds { get; set; }
        public required IEnumerable<int> WorkDescriptionsIds { get; set; }
        public IEnumerable<Problem>? Problems { get; set; }
        public IEnumerable<WorkDescription>? WorkDescriptiones { get; set; }
    }
}
