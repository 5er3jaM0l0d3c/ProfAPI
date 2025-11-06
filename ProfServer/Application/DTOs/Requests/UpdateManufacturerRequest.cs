namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateManufacturerRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}