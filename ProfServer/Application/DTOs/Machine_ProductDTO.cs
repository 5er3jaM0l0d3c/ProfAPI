using ProfServer.Models;

namespace ProfServer.Application.DTOs
{
    public class Machine_ProductDTO
    {
        public int Id { get; set; }
        public MachineDTO Machine { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
