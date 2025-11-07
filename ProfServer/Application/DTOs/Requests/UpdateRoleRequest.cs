namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateRoleRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}