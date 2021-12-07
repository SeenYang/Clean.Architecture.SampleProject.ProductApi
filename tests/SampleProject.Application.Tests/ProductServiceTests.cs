using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using SampleProject.Application.Services;
using SampleProject.Domain.Interface;
using SampleProject.Domain.Models;
using Xunit;

namespace SampleProject.Application.Tests
{
    public class GivenProductService
    {
        private Mock<ILogger<ProductService>> _logger;
        private Mock<IMessageQueueClient> _client;
        private Mock<IProductRepository> _productRepo;
        private readonly IProductService _service;

        public GivenProductService()
        {
            _logger = new Mock<ILogger<ProductService>>();
            _client = new Mock<IMessageQueueClient>();
            _productRepo = new Mock<IProductRepository>();

            _service = new ProductService(_logger.Object, _client.Object, _productRepo.Object);
        }

        // TODO: Try AutoFixture.
        [Fact]
        public async Task WhenGivenNameExists_GetProductsByNameShouldReturnRespectiveProducts()
        {
            _productRepo.Setup(x => x.GetProductsByName(It.IsAny<string>()))
                .ReturnsAsync(new List<ProductModel>
                {
                    new() { Id = Guid.NewGuid() },
                    new() { Id = Guid.NewGuid() },
                });

            var result = await _service.GetProductsByName("Tests");

            Assert.NotNull(result);
            Assert.Equal(2, ((List<ProductModel>)result).Count);
        }

        [Fact]
        public async Task WhenProductExists_GetProductByIdShouldReturnRespectiveProduct()
        {
            var fakeId = Guid.NewGuid();
            _productRepo.Setup(x => x.GetProductById(fakeId))
                .ReturnsAsync(new ProductModel { Id = fakeId });

            var result = await _service.GetProductById(fakeId);

            Assert.NotNull(result);
            Assert.Equal(fakeId, result.Id);
        }

        [Fact]
        public async Task WhenProductExists_GetProductBySkuShouldReturnRespectiveProduct()
        {
            var fakeSku = "asdfhjlqwehrlqkwejnl";
            _productRepo.Setup(x => x.GetProductBySku(fakeSku))
                .ReturnsAsync(new ProductModel { Sku = fakeSku });

            var result = await _service.GetProductBySku(fakeSku);

            Assert.NotNull(result);
            Assert.Equal(fakeSku, result.Sku);
        }

        [Fact]
        public async Task WhenAddNewProduct_ShouldReturnNewProductAndSendMessage()
        {
            var product = new ProductModel
            {
                Sku = "FakeSku",
                Name = "New Product"
            };
            _productRepo.Setup(x => x.AddProduct(It.IsAny<ProductModel>()))
                .ReturnsAsync((ProductModel input) =>
                {
                    input.Id = Guid.NewGuid();
                    return input;
                });

            var result = await _service.AddProduct(product);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            _client.Verify(c => c.SendMessage(MessageType.Create, It.IsAny<ProductModel>()), Times.Once);
            _logger.VerifyLog(l => l.LogInformation("Product {Id} create successfully", result.Id), Times.Once);
        }
        
        [Fact]
        public async Task WhenUpdateNewProduct_ShouldReturnNewProductAndSendMessage()
        {
            var product = new ProductModel
            {
                Sku = "FakeSku",
                Name = "New Product"
            };
            _productRepo.Setup(x => x.UpdateProduct(It.IsAny<ProductModel>()))
                .ReturnsAsync((ProductModel input) => input);

            var result = await _service.UpdateProduct(product);

            Assert.NotNull(result);
            _client.Verify(c => c.SendMessage(MessageType.Update, It.IsAny<ProductModel>()), Times.Once);
            _logger.VerifyLog(l => l.LogInformation("Product {Id} update successfully", result.Id), Times.Once);
        }
    }
}