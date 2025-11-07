namespace ProfServer.Application.Interfaces
{
    public class CreateMachine_ProductRequest
    {
        public int MachineId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}