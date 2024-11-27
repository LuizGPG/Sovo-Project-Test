using SovosProjectTest.Domain.Entities;
using SovosProjectTest.Domain.Filters;

namespace SovosProjectTest.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task Create(Product productModel);
        Task Update(Product productModel);
        Task<IList<Product>> GetProductsAll();
        Task<Product> GetByIdAsync(Guid id);
        Task Delete(Guid id);
        Task<(IList<Product> Products, int TotalCount)> GetProducts(ProductFilter productFilterDto);
    }
}
