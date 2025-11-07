namespace ProfServer.Application.DTOs.Requests
{
    public class AuthRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}