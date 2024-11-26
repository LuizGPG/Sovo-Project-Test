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

        public async Task<(IList<Product> Products, int TotalCount)> GetProducts(ProductFilterDto productFilterDto)
        {
            var query = _dbContext.Products.AsQueryable();
            
            if (!string.IsNullOrEmpty(productFilterDto.Category))
            {
                query = query.Where(p => p.Category == productFilterDto.Category);
            }

            if (productFilterDto.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= productFilterDto.MinPrice.Value);
            }

            if (productFilterDto.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= productFilterDto.MaxPrice.Value);
            }

            if (productFilterDto.SortByPrice)
            {
                query = query.OrderBy(p => p.Price);
            }
            
            var totalCount = await query.CountAsync();
            if (productFilterDto.Page.HasValue && productFilterDto.PageSize.HasValue)
            {
                query = query
                    .Skip((productFilterDto.Page.Value - 1) * productFilterDto.PageSize.Value)
                    .Take(productFilterDto.PageSize.Value);
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
