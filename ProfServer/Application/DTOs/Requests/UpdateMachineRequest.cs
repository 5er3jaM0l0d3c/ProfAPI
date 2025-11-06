using ProfServer.Models;

namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateMachineRequest
    {
        public int Id { get; set; }
        public required string Address { get; set; }
        public int PaymentTypeId { get; set; }
        public required string SerialNumber { get; set; }
        public required string InventoryNumber { get; set; }
        public int ManufacturerId { get; set; }
        public DateOnly ManufactureDate { get; set; }
        public DateOnly BeginExplotationDate { get; set; }
        public int InterverificationIntervalMonth { get; set; }
        public int ResourceHours { get; set; }
        public int ServiceTime { get; set; }
        public int StatusId { get; set; }
        public int ManufactureCountryId { get; set; }
        public DateOnly InventarizationDate { get; set; }
    }
}