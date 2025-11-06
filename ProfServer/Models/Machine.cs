namespace ProfServer.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public required string Address { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; } = null!;
        public required string SerialNumber { get; set; }
        public required string InventoryNumber { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; } = null!;
        public DateOnly ManufactureDate { get; set; }
        public DateOnly BeginExplotationDate { get; set; }
        public int InterverificationInvervalMonths { get; set; }
        public int ResourceHours { get; set; }
        public int ServiceTime { get; set; }
        public int StatusId { get; set; }
        public MachineStatus Status { get; set; } = null!;
        public int ManufactureCountryId { get; set; }
        public ManufactureCountry ManufactureCountry { get; set; } = null!;
        public DateOnly? InventarizationDate { get; set; }
        public DateOnly? NextMaintenanceDate { get; set; }
        public decimal? TotalIncome { get; set; }
        public DateOnly AddingDate { get; set; }
    }
}
