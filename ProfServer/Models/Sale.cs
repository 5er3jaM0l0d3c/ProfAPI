namespace ProfServer.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; } = null!;
        public decimal? TotalPrice { get; set; }
    }
}
