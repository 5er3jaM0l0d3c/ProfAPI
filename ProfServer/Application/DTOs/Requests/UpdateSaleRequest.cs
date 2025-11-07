using ProfServer.Models;

namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateSaleRequest
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
