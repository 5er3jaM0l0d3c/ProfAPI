namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateMachineStatusRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}