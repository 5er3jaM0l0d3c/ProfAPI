using ProfServer.Models;

namespace ProfServer.Application.DTOs.Requests
{
    public class CreateUserRequest
    {
        public required string Surname { get; set; }
        public required string Name { get; set; }
        public required string Patronymic { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public int RoleId { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }   
    }
}