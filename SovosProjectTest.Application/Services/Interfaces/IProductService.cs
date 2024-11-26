using SovosProjectTest.Application.Filters;
using SovosProjectTest.Application.Model;

namespace SovosProjectTest.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task Create(ProductModel productModel);
        Task Update(ProductModel productModel);
        Task Delete(Guid id);
        Task<IList<ProductModel>> GetProductsAll();
        Task<PagedResponse<ProductModel>> GetProducts(ProductFilterModel productFilter);
        Task<ProductModel> GetByIdAsync(Guid id);
    }
}
