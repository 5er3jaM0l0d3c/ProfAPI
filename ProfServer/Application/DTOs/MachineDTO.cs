using ProfServer.Models;

namespace ProfServer.Application.DTOs
{
    public class MachineDTO
    {
        public int Id { get; set; }
        public required string Address { get; set; }
        public PaymentType PaymentType { get; set; } = null!;
        public required string SerialNumber { get; set; }
        public required string InventoryNumber { get; set; }
        public Manufacturer Manufacturer { get; set; } = null!;
        public DateOnly ManufactureDate { get; set; }
        public DateOnly BeginExplotationDate { get; set; }
        public int InterverificationIntervalMonth { get; set; }
        public int ResourceHours { get; set; }
        public int ServiceTime { get; set; }
        public MachineStatus Status { get; set; } = null!;
        public ManufactureCountry ManufactureCountry { get; set; } = null!;
        public DateOnly InventarizationDate { get; set; }
        public DateOnly? NextMaintenanceDate { get; set; }
        public decimal? TotalIncome { get; set; }
        public DateOnly AddingDate { get; set; }
    }
}