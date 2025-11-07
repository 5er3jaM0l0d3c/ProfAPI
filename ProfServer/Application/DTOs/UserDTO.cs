using ProfServer.Models;

namespace ProfServer.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Surname { get; set; }
        public required string Name { get; set; }
        public required string Patronymic { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public Role Role { get; set; } = null!;
        public required string Login { get; set; }
    }
}