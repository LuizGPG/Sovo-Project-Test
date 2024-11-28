using SovosProjectTest.Application.Model;
using SovosProjectTest.Domain.Entities;

namespace SovosProjectTest.UnitTest.Fake
{
    public static class ProductFake
    {

        public static ProductModel ProductModelFakeData()
        {
            return new ProductModel { Id = Guid.NewGuid(), Name = "Product 1", Category = "Test Category 1", Price = 100 , StockQuantity = 10, RowVersion = Guid.NewGuid() };
        }

        public static Product ProductFakeData()
        {
            return new Product { Id = Guid.NewGuid(), Name = "Product 1", Category = "Test Category 1", Price = 100, StockQuantity = 10, RowVersion = Guid.NewGuid() };
        }

        public static List<ProductModel> ProductModelFakeListData()
        {
            return new List<ProductModel>
                {
                    new ProductModel { Id = Guid.NewGuid(), Name = "Product 1", Category = "Test Category 1", Price = 100, StockQuantity = 10, RowVersion = Guid.NewGuid() },
                    new ProductModel { Id = Guid.NewGuid(), Name = "Product 2", Category = "Test Category 2", Price = 200, StockQuantity = 10, RowVersion = Guid.NewGuid() },
                    new ProductModel { Id = Guid.NewGuid(), Name = "Product 3", Category = "Test Category 1", Price = 70, StockQuantity = 10, RowVersion = Guid.NewGuid() },

                };
        }

        public static List<Product> ProductFakeListData()
        {
            return new List<Product>
                {
                    new Product { Id = Guid.NewGuid(), Name = "Product 1", Category = "Test Category 1", Price = 100, StockQuantity = 10, RowVersion = Guid.NewGuid() },
                    new Product { Id = Guid.NewGuid(), Name = "Product 2", Category = "Test Category 2", Price = 200, StockQuantity = 10, RowVersion = Guid.NewGuid() },
                    new Product { Id = Guid.NewGuid(), Name = "Product 3", Category = "Test Category 1", Price = 70, StockQuantity = 10, RowVersion = Guid.NewGuid() },
                };
        }

    }
}
