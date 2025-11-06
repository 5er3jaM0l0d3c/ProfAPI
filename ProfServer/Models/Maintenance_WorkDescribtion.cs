namespace ProfServer.Models
{
    public class Maintenance_WorkDescribtion
    {
        public int Id { get; set; }
        public int MaintenanceId { get; set; }
        public Maintenance Maintenance { get; set; } = null!;
        public int WorkDescribtionId { get; set; }
        public WorkDescribtion WorkDescribtion { get; set; } = null!;
    }
}
