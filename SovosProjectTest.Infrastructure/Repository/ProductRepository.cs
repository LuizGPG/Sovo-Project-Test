using Microsoft.EntityFrameworkCore;
using SovosProjectTest.Domain.Entities;
using SovosProjectTest.Domain.Filters;
using SovosProjectTest.Domain.Interfaces;
using SovosProjectTest.Infrastructure.Data;

namespace SovosProjectTest.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Product productModel)
        {
            _dbContext.Products.Add(productModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await _dbContext.Products.FirstAsync(d => d.Id == id);

            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<(IList<Product> Products, int TotalCount)> GetProducts(ProductFilter productFilter)
        {
            var query = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(productFilter.Category))
            {
                query = query.Where(p => p.Category == productFilter.Category);
            }

            if (productFilter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= productFilter.MinPrice.Value);
            }

            if (productFilter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= productFilter.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(productFilter.SortBy))
            {
                query = productFilter.SortBy.ToLower()
                switch
                {
                    "price" => productFilter.SortDescending ? 
                        query.OrderByDescending(p => (double)p.Price) : query.OrderBy(p => (double)p.Price),
                    "name" => productFilter.SortDescending ? 
                        query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    _ => query
                };
            }

            var totalCount = await query.CountAsync();
            if (productFilter.Page.HasValue && productFilter.PageSize.HasValue)
            {
                query = query
                    .Skip((productFilter.Page.Value - 1) * productFilter.PageSize.Value)
                    .Take(productFilter.PageSize.Value);
            }

            var products = await query.ToListAsync();
            return (products, totalCount);
        }

        public async Task<IList<Product>> GetProductsAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task Update(Product productModel)
        {
            _dbContext.Update(productModel);
            await _dbContext.SaveChangesAsync();
        }
    }
}
