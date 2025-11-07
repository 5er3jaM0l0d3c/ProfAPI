using ProfServer.Application.DTOs;

namespace ProfServer.Models
{
    public class Maintenance_WorkDescription
    {
        public int Id { get; set; }
        public int MaintenanceId { get; set; }
        public MaintenanceDTO Maintenance { get; set; } = null!;
        public int WorkDescribtionId { get; set; }
        public WorkDescription WorkDescribtion { get; set; } = null!;
    }
}
