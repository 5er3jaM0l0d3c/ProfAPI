namespace ProfServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Surname { get; set; }
        public required string Name { get; set; }
        public required string Patronymic { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public string Login { get; set; } = string.Empty;
        public required byte[] Password { get; set; } 
    }
}