namespace ProfServer.Models
{
    public class Maintenance_Problem
    {
        public int Id { get; set; }
        public int MaintenanceId { get; set; }
        public Maintenance Maintenance { get; set; } = null!;
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = null!;
    }
}
