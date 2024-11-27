using SovosProjectTest.Application.Filters;
using SovosProjectTest.Application.Model;

namespace SovosProjectTest.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductModel productModel);
        Task UpdateAsync(ProductModel productModel);
        Task DeleteAsync(Guid id);
        Task<PagedResponse<ProductModel>> GetProductsAsync(ProductFilterModel productFilter);
        Task<ProductModel> GetByIdAsync(Guid id);
    }
}
