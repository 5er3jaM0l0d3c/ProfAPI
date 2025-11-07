namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateProblemRequest
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}