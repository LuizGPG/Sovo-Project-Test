using Microsoft.EntityFrameworkCore;
using SovosProjectTest.Domain.Filters;
using SovosProjectTest.Infrastructure.Data;
using SovosProjectTest.Infrastructure.Repository;
using SovosProjectTest.UnitTest.Fake;
using Xunit;

namespace SovosProjectTest.UnitTest.RepositoryTest
{
    public class ProductRepositoryTest
    {
        private readonly ProductRepository _productRepository;
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public ProductRepositoryTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductDbTest")
                .Options;

            var dbContext = new ApplicationDbContext(_dbContextOptions);
            _productRepository = new ProductRepository(dbContext);
            
            dbContext.Products.AddRange(ProductFake.ProductFakeListData());
            dbContext.SaveChanges();
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
