using Microsoft.AspNetCore.Mvc;
using SovosProjectTest.Application;
using SovosProjectTest.Application.Filters;
using SovosProjectTest.Application.Model;
using SovosProjectTest.Application.Services.Interfaces;
using SovosProjectTest.Domain.Entities;
using SovosProjectTest.Domain.Filters;

namespace SovosProjectTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private const string NotFoundMessage = "No products found with the given criteria.";
        private const string InvalidStockPriceMessage = "Invalid stock quantity or price.";

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("GetProductsFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedResponse<ProductModel>>> GetProductsFilter([FromBody] ProductFilterModel productFilter)
        {
            var productResponde = await _productService.GetProducts(productFilter);
            return Ok(productResponde);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<ProductModel>>> Create([FromBody] ProductModel product)
        {
            if(product.StockQuantity <= 0 || product.Price <= 0)
            {
                return BadRequest(new { Message = InvalidStockPriceMessage });
            }
            
            await _productService.Create(product);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] ProductModel productModel)
        {
            var product = await _productService.GetByIdAsync(productModel.Id);
            if (product == null)
            {
                return NotFound(new { Message = NotFoundMessage });
            }

            if (productModel.StockQuantity <= 0 || productModel.Price <= 0)
            {
                return BadRequest(new { Message = InvalidStockPriceMessage });
            }

            await _productService.Update(productModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product == null)
            {
                return NotFound(new { Message = NotFoundMessage });
            }

            await _productService.Delete(id);
            return Ok();
        }

    }
}
