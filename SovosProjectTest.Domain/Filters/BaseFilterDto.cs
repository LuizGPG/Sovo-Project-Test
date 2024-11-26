namespace SovosProjectTest.Domain.Filters
{
    public class BaseFilterDto
    {
        public bool SortByPrice { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
