namespace ProfServer.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; } = null!;
        public DateOnly Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
