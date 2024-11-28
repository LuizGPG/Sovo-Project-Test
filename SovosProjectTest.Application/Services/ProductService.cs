using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SovosProjectTest.Application.Filters;
using SovosProjectTest.Application.Model;
using SovosProjectTest.Application.Services.Interfaces;
using SovosProjectTest.Domain.Entities;
using SovosProjectTest.Domain.Filters;
using SovosProjectTest.Domain.Interfaces;

namespace SovosProjectTest.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            product.RowVersion = Guid.NewGuid();
            await _productRepository.CreateAsync(product);
        }

        public async Task<ProductModel> UpdateAsync(ProductModel productModel)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productModel.Id);

            if (existingProduct.RowVersion != productModel.RowVersion)
            {
                throw new DbUpdateConcurrencyException("Product was updated by another process.");
            }

            var product = _mapper.Map<Product>(productModel);
            product.RowVersion = Guid.NewGuid();
            var updatedProduct = await _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductModel>(updatedProduct);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<PagedResponse<ProductModel>> GetProductsAsync(ProductFilterModel productFilterModel)
        {
            var productFilter = _mapper.Map<ProductFilter>(productFilterModel);
            var (products, totalCount) = await _productRepository.GetProductsAsync(productFilter);

            var productModels = _mapper.Map<List<ProductModel>>(products);
            return new PagedResponse<ProductModel>
            {
                Data = productModels,
                TotalItems = totalCount,
                PageNumber = productFilterModel.Page,
                PageSize = productFilterModel.PageSize
            };
        }

        public async Task<ProductModel> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductModel>(product);
        }
    }
}
