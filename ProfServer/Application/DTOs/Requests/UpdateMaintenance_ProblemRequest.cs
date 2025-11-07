namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateMaintenance_ProblemRequest
    {
        public int Id { get; set; }
        public int MaintenanceId { get; set; }
        public int ProblemId { get; set; }
    }
}