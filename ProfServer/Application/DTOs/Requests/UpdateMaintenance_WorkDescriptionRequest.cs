namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateMaintenance_WorkDescriptionRequest
    {
        public int Id { get; set; }
        public int MaintenanceId { get; set; }
        public int WorkDescribtionId { get; set; }
    }
}