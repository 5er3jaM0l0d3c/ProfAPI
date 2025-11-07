namespace ProfServer.Application.Interfaces
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int MinimalStockQuantity { get; set; }
    }
}