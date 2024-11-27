using AutoMapper;
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

        public async Task Create(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            await _productRepository.Create(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public async Task<ProductModel> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductModel>(product);
        }

        public async Task<PagedResponse<ProductModel>> GetProducts(ProductFilterModel productFilterModel)
        {
            var productFilter = _mapper.Map<ProductFilter>(productFilterModel);
            var (products, totalCount) = await _productRepository.GetProducts(productFilter);

            var productModels = _mapper.Map<List<ProductModel>>(products);
            return new PagedResponse<ProductModel>
            {
                Data = productModels,
                TotalItems = totalCount,
                PageNumber = productFilterModel.Page,
                PageSize = productFilterModel.PageSize
            };
        }

        public async Task<IList<ProductModel>> GetProductsAll()
        {
            var product = await _productRepository.GetProductsAll();
            var productModel = _mapper.Map<List<ProductModel>>(product);

            return productModel;
        }

        public async Task Update(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            await _productRepository.Update(product);
        }
    }
}
