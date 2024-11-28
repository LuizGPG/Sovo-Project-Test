namespace SovosProjectTest.Application.Model
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public Guid RowVersion { get; set; }
    }
}
