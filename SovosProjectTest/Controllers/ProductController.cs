using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SovosProjectTest.Application;
using SovosProjectTest.Application.Filters;
using SovosProjectTest.Application.Model;
using SovosProjectTest.Application.Services.Interfaces;

namespace SovosProjectTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private const string NotFoundMessage = "No products found with the given criteria.";
        private const string InvalidStockPriceMessage = "Invalid stock quantity or price.";
        private const string HasProductIdMessage = "Product with this ID already exists.";
        private const string ConflitMessage = "The product was updated by another user. Please refresh and try again.";
        private readonly IProductService _productService;
        private readonly IMemoryCache _memoryCache;

        public ProductController(IProductService productService, IMemoryCache memoryCache)
        {
            _productService = productService;
            _memoryCache = memoryCache;
        }

        [HttpPost("GetProductsFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedResponse<ProductModel>>> GetProductsFilter([FromBody] ProductFilterModel productFilter)
        {
            var cacheKey = $"Products_{productFilter.Page}_{productFilter.PageSize}_{productFilter.Category}_{productFilter.MinPrice}_{productFilter.MaxPrice}_{productFilter.SortBy}_{productFilter.SortDescending}";

            if (!_memoryCache.TryGetValue(cacheKey, out PagedResponse<ProductModel> cachedProducts))
            {
                cachedProducts = await _productService.GetProductsAsync(productFilter);
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                };

                _memoryCache.Set(cacheKey, cachedProducts, cacheOptions);
            }

            if (cachedProducts == null || !cachedProducts.Data.Any())
            {
                return NotFound(new { Message = NotFoundMessage });
            }

            return Ok(cachedProducts);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] ProductModel productModel)
        {
            if (productModel.Id != default)
            {
                var product = _productService.GetByIdAsync(productModel.Id);
                if(product.Result !=  null)
                {
                    return BadRequest(new { Message = HasProductIdMessage });
                }
            }

            if (productModel.StockQuantity < 0 || productModel.Price <= 0)
            {
                return BadRequest(new { Message = InvalidStockPriceMessage });
            }

            await _productService.CreateAsync(productModel);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductModel>> Update([FromBody] ProductModel productModel)
        {
            var product = await _productService.GetByIdAsync(productModel.Id);
            if (product == null)
            {
                return NotFound(new { Message = NotFoundMessage });
            }

            if (productModel.StockQuantity < 0 || productModel.Price <= 0)
            {
                return BadRequest(new { Message = InvalidStockPriceMessage });
            }

            try
            {
                var updatedProduct = await _productService.UpdateAsync(productModel);
                return Ok(updatedProduct);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(ConflitMessage);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { Message = NotFoundMessage });
            }

            await _productService.DeleteAsync(id);
            return Ok();
        }

    }
}
