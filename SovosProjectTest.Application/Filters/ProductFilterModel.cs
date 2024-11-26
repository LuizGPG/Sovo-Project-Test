namespace SovosProjectTest.Application.Filters
{
    public class ProductFilterModel : BaseFilterModel
    {
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
