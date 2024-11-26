namespace SovosProjectTest.Domain.Filters
{
    public class ProductFilterDto : BaseFilterDto
    {
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        
    }
}
