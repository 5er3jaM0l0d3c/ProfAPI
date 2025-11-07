namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateMaintenanceRequest
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int UserId { get; set; }
        public required IEnumerable<int> ProblemsIds { get; set; }
        public required IEnumerable<int> WorkDescriptionsIds { get; set; }
    }
}