using SovosProjectTest.Domain.Entities;
using SovosProjectTest.Domain.Filters;

namespace SovosProjectTest.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task CreateAsync(Product productModel);
        Task UpdateAsync(Product productModel);
        Task DeleteAsync(Guid id);
        Task<(IList<Product> Products, int TotalCount)> GetProductsAsync(ProductFilter productFilter);
        Task<Product> GetByIdAsync(Guid id);
    }
}
