namespace ProfServer.Application.DTOs.Requests
{
    public class UpdateManufactureCountryRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}