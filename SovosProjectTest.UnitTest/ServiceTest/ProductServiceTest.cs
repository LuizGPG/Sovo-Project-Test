﻿using AutoMapper;
using Moq;
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
                .Setup(repo => repo.Create(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            await _productService.Create(productModelFake);

            _mapperMock.Verify(mapper => mapper.Map<Product>(productModelFake), Times.Once);
            _productRepositoryMock.Verify(repo => repo.Create(It.Is<Product>(p =>
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
            var productFilerDto = ProductFilterFake.ProductFilterFakeData();

            var totalCountFake = productsFake.Count();
            _mapperMock
                .Setup(mapper => mapper.Map<ProductFilter>(productFilerModel))
                .Returns(productFilerDto);

            _mapperMock
                .Setup(mapper => mapper.Map<List<ProductModel>>(productsFake))
                .Returns(productsModelFake);

            _productRepositoryMock
                .Setup(repo => repo.GetProducts(productFilerDto))
                .ReturnsAsync((productsFake, totalCountFake));

            var result = await _productService.GetProducts(productFilerModel);

            Assert.NotNull(result);
            Assert.Equal(totalCountFake, result.TotalItems);
            Assert.Equal(productFilerModel.Page, result.PageNumber);
            Assert.Equal(productFilerModel.PageSize, result.PageSize);
            Assert.Equal(productsModelFake, result.Data);

            _mapperMock.Verify(mapper => mapper.Map<ProductFilter>(productFilerModel), Times.Once);
            _productRepositoryMock.Verify(repo => repo.GetProducts(productFilerDto), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<List<ProductModel>>(productsFake), Times.Once);
        }
    }
}
