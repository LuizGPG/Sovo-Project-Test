using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using SovosProjectTest.Application.Filters;
using SovosProjectTest.Application.Model;
using SovosProjectTest.Application.Services;
using SovosProjectTest.Domain.Entities;
using SovosProjectTest.Domain.Filters;
using SovosProjectTest.Domain.Interfaces;
using SovosProjectTest.UnitTest.Fake;
using Xunit;

namespace SovosProjectTest.UnitTest.ServiceTest
{
    public class ProductServiceTest
    {
        private readonly ProductService _productService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;

        public ProductServiceTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();

            _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Create_ShouldCallRepositoryWithMappedProduct()
        {
            var productModelFake = ProductFake.ProductModelFakeData();
            var productFake = ProductFake.ProductFakeData();

            _mapperMock
                .Setup(mapper => mapper.Map<Product>(productModelFake))
                .Returns(productFake);

            _productRepositoryMock
                .Setup(repo => repo.CreateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            await _productService.CreateAsync(productModelFake);

            _mapperMock.Verify(mapper => mapper.Map<Product>(productModelFake), Times.Once);
            _productRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<Product>(p =>
                p.Id == productFake.Id &&
                p.Name == productFake.Name &&
                p.Price == productFake.Price &&
                p.StockQuantity == productFake.StockQuantity
            )), Times.Once);
        }

        [Fact]
        public async Task GetProducts_ShouldCallRepositoryWithMappedProductReturningMapp()
        {
            var productsFake = ProductFake.ProductFakeListData();
            var productsModelFake = ProductFake.ProductModelFakeListData();
            var productFilerModel = ProductFilterFake.ProductFilterModelFake();
            var productFilter = ProductFilterFake.ProductFilterFakeData();

            var totalCountFake = productsFake.Count();
            _mapperMock
                .Setup(mapper => mapper.Map<ProductFilter>(productFilerModel))
                .Returns(productFilter);

            _mapperMock
                .Setup(mapper => mapper.Map<List<ProductModel>>(productsFake))
                .Returns(productsModelFake);

            _productRepositoryMock
                .Setup(repo => repo.GetProductsAsync(productFilter))
                .ReturnsAsync((productsFake, totalCountFake));

            var result = await _productService.GetProductsAsync(productFilerModel);

            Assert.NotNull(result);
            Assert.Equal(totalCountFake, result.TotalItems);
            Assert.Equal(productFilerModel.Page, result.PageNumber);
            Assert.Equal(productFilerModel.PageSize, result.PageSize);
            Assert.Equal(productsModelFake, result.Data);

            _mapperMock.Verify(mapper => mapper.Map<ProductFilter>(productFilerModel), Times.Once);
            _productRepositoryMock.Verify(repo => repo.GetProductsAsync(productFilter), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<List<ProductModel>>(productsFake), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ShouldThrowDbUpdateConcurrencyException()
        {
            var productModel = ProductFake.ProductModelFakeData();
            var existingProduct = ProductFake.ProductFakeData();

            _productRepositoryMock
                .Setup(r => r.GetByIdAsync(productModel.Id))
                .ReturnsAsync(existingProduct);

            var exception = await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                await _productService.UpdateAsync(productModel);
            });

            Assert.Equal("Product was updated by another process.", exception.Message);
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotThrowDbUpdateConcurrencyException()
        {
            var productModel = ProductFake.ProductModelFakeData();
            var existingProduct = ProductFake.ProductFakeData();

            _mapperMock
                .Setup(mapper => mapper.Map<Product>(productModel))
                .Returns(existingProduct);

            _productRepositoryMock
                .Setup(r => r.GetByIdAsync(productModel.Id))
                .ReturnsAsync(existingProduct);

            _productRepositoryMock
                .Setup(r => r.UpdateAsync(existingProduct))
                .ReturnsAsync(existingProduct);

            _mapperMock
                .Setup(mapper => mapper.Map<ProductModel>(existingProduct))
                .Returns(productModel);

            var oldVersion = productModel.RowVersion;

            productModel.RowVersion = existingProduct.RowVersion;
            var exception = await Record.ExceptionAsync(async () =>
            {
                productModel = await _productService.UpdateAsync(productModel);
            });

            Assert.NotEqual(oldVersion, productModel.RowVersion);
            Assert.Null(exception);
            _productRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}

