namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateWorkDescriptionRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}