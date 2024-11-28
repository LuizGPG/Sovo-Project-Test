using Microsoft.EntityFrameworkCore;
using SovosProjectTest.Domain.Entities;
using SovosProjectTest.Infrastructure.Data;
using SovosProjectTest.Infrastructure.Repository;
using SovosProjectTest.UnitTest.Fake;
using Xunit;

namespace SovosProjectTest.UnitTest.RepositoryTest
{
    public class ProductRepositoryTest
    {
        private readonly ProductRepository _productRepository;
        private readonly ApplicationDbContext _dbContext;

        public ProductRepositoryTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductDbTest")
                .Options;

            _dbContext = new ApplicationDbContext(dbContextOptions);
            _productRepository = new ProductRepository(_dbContext);

            _dbContext.Products.AddRange(ProductFake.ProductFakeListData());
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetProducts_ShouldReturnFilteredAndSortedResults_Desc()
        {
            var productFilter = ProductFilterFake.ProductFilterFakeData();

            var (products, totalCount) = await _productRepository.GetProductsAsync(productFilter);

            Assert.NotNull(products);
            Assert.True(products[0].Price >= products[1].Price);
            Assert.All(products, p => Assert.Equal("Test Category 1", p.Category));
        }

        [Fact]
        public async Task GetProducts_ShouldReturnFilteredAndSortedResults()
        {
            var productFilter = ProductFilterFake.ProductFilterFakeData();
            productFilter.SortDescending = false;

            var (products, totalCount) = await _productRepository.GetProductsAsync(productFilter);

            Assert.NotNull(products);
            Assert.True(products[0].Price <= products[1].Price);
            Assert.All(products, p => Assert.Equal("Test Category 1", p.Category));
        }
    }
}
