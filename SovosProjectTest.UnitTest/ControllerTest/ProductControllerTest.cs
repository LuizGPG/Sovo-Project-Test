using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using SovosProjectTest.Application.Filters;
using SovosProjectTest.Application.Model;
using SovosProjectTest.Application;
using SovosProjectTest.Application.Services.Interfaces;
using SovosProjectTest.Controllers;
using Xunit;
using SovosProjectTest.UnitTest.Fake;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SovosProjectTest.UnitTest.ControllerTest
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly IMemoryCache _memoryCache;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _productServiceMock = new Mock<IProductService>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _controller = new ProductController(_productServiceMock.Object, _memoryCache);
        }

        [Fact]
        public async Task GetProductsFilter_ShouldReturnOk_WhenProductsAreCached()
        {
            var productFilter = new ProductFilterModel { Page = 1, PageSize = 10 };
            var expectedProducts = new PagedResponse<ProductModel>
            {
                Data = ProductFake.ProductModelFakeListData(),
                PageNumber = 1,
                PageSize = 10,
                TotalItems = 2
            };

            _memoryCache.Set($"Products_1_10_____False", expectedProducts);

            var result = await _controller.GetProductsFilter(productFilter);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProducts = Assert.IsType<PagedResponse<ProductModel>>(okResult.Value);

            Assert.Equal(expectedProducts.TotalItems, returnedProducts.TotalItems);
            Assert.Equal(expectedProducts.Data.Count, returnedProducts.Data.Count);
        }

        [Fact]
        public async Task GetProductsFilter_ShouldCallService_WhenCacheIsEmpty()
        {
            var productFilter = new ProductFilterModel { Page = 1, PageSize = 10 };
            var expectedProducts = new PagedResponse<ProductModel>
            {
                Data = ProductFake.ProductModelFakeListData(),
                PageNumber = 1,
                PageSize = 10,
                TotalItems = 1
            };

            _productServiceMock
                .Setup(service => service.GetProductsAsync(productFilter))
                .ReturnsAsync(expectedProducts);

            var result = await _controller.GetProductsFilter(productFilter);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProducts = Assert.IsType<PagedResponse<ProductModel>>(okResult.Value);

            Assert.Equal(expectedProducts.Data.Count, returnedProducts.Data.Count);
            _productServiceMock.Verify(service => service.GetProductsAsync(It.IsAny<ProductFilterModel>()), Times.Once);
        }

        [Fact]
        public async Task GetProductsFilter_ShouldReturnNotFound()
        {
            var productFilter = ProductFilterFake.ProductFilterModelFake();
            var expectedProducts = new PagedResponse<ProductModel>
            {
                PageNumber = 1,
                PageSize = 10,
                TotalItems = 2
            };
            
            _productServiceMock
                .Setup(service => service.GetProductsAsync(productFilter))
                .ReturnsAsync(expectedProducts);

            _memoryCache.Set($"Products_1_10_Category Test_10_100__False", expectedProducts);

            var result = await _controller.GetProductsFilter(productFilter);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateProducts_ShouldReturnBadRequest()
        {
            var productModelFake = ProductFake.ProductModelFakeData();
            productModelFake.Price = 0;
            productModelFake.StockQuantity = -1;

            var result = await _controller.Create(productModelFake);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public async Task CreateProducts_ShouldReturnOk()
        {
            var productModelFake = ProductFake.ProductModelFakeData();
            
            _productServiceMock
                .Setup(service => service.GetByIdAsync(productModelFake.Id))
                .ReturnsAsync((ProductModel) null);

            _productServiceMock
                .Setup(service => service.CreateAsync(productModelFake))
                .Returns(Task.CompletedTask);

            var result = await _controller.Create(productModelFake);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task CreateProducts_ShouldReturnBadRequest_DuplicatedId()
        {
            var productModelFake = ProductFake.ProductModelFakeData();

            _productServiceMock
                .Setup(service => service.GetByIdAsync(productModelFake.Id))
                .ReturnsAsync(productModelFake);

            _productServiceMock
                .Setup(service => service.CreateAsync(productModelFake))
                .Returns(Task.CompletedTask);

            var result = await _controller.Create(productModelFake);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public async Task UpdateProducts_ShouldReturnOk()
        {
            var productModelFake = ProductFake.ProductModelFakeData();
            
            _productServiceMock
                .Setup(service => service.GetByIdAsync(productModelFake.Id))
                .ReturnsAsync(productModelFake);

            _productServiceMock
                .Setup(service => service.UpdateAsync(productModelFake))
                .Returns(Task.CompletedTask);

            var result = await _controller.Update(productModelFake);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateProducts_ShouldReturnNotFound()
        {
            var productModelFake = ProductFake.ProductModelFakeData();

            _productServiceMock
                .Setup(service => service.GetByIdAsync(productModelFake.Id))
                .ReturnsAsync((ProductModel)null);

            _productServiceMock
                .Setup(service => service.UpdateAsync(productModelFake))
                .Returns(Task.CompletedTask);

            var result = await _controller.Update(productModelFake);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateProducts_ShouldReturnBadRequest()
        {
            var productModelFake = ProductFake.ProductModelFakeData();
            productModelFake.Price = 0;
            productModelFake.StockQuantity = 0;

            var result = await _controller.Create(productModelFake);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task DeleteProducts_ShouldReturnOk()
        {
            var productModelFake = ProductFake.ProductModelFakeData();

            _productServiceMock
                .Setup(service => service.GetByIdAsync(productModelFake.Id))
                .ReturnsAsync(productModelFake);

            _productServiceMock
                .Setup(service => service.DeleteAsync(productModelFake.Id))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(productModelFake.Id);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteProducts_ShouldReturnNotFound()
        {
            var productModelFake = ProductFake.ProductModelFakeData();

            _productServiceMock
                .Setup(service => service.GetByIdAsync(productModelFake.Id))
                .ReturnsAsync((ProductModel)null);

            _productServiceMock
                .Setup(service => service.DeleteAsync(productModelFake.Id))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(productModelFake.Id);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }
    }
}
