using SovosProjectTest.Application.Filters;
using SovosProjectTest.Domain.Filters;

namespace SovosProjectTest.UnitTest.Fake
{
    public static class ProductFilterFake
    {
        public static ProductFilterModel ProductFilterModelFake()
        {
            return new ProductFilterModel()
            {
                Category = "Test Category 1",
                MaxPrice = 100,
                MinPrice = 10,
            };
        }

        public static ProductFilter ProductFilterFakeData()
        {
            return new ProductFilter()
            {
                Category = "Test Category 1",
                MaxPrice = 200,
                MinPrice = 50,
                SortBy = "price",
                SortDescending = true
            };
        }
    }
}
