namespace ProfServer.Application.Interfaces
{
    public class UpdateMachine_ProductRequest
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}