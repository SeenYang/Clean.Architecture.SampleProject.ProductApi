using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SampleProject.Domain.Interface;
using SampleProject.Domain.Models;

namespace SampleProject.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMessageQueueClient _msgClient;
        private readonly IProductRepository _productRepo;

        public ProductService(ILogger<ProductService> logger, IMessageQueueClient msgClient,
            IProductRepository productRepo)
        {
            _logger = logger;
            _msgClient = msgClient;
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByName(string name)
        {
            return await _productRepo.GetProductsByName(name);
        }

        public async Task<ProductModel> GetProductBySku(string sku)
        {
            return await _productRepo.GetProductBySku(sku);
        }

        public async Task<ProductModel> GetProductById(Guid sku)
        {
            return await _productRepo.GetProductById(sku);
        }

        public async Task<ProductModel> AddProduct(ProductModel product)
        {
            var newProduct = await _productRepo.AddProduct(product);
            _logger.LogInformation("Product {Id} create successfully", newProduct.Id);
            await _msgClient.SendMessage(MessageType.Create, newProduct);
            return newProduct;
        }

        public async Task<ProductModel> UpdateProduct(ProductModel product)
        {
            var newProduct = await _productRepo.UpdateProduct(product);
            _logger.LogInformation("Product {Id} update successfully", newProduct.Id);
            await _msgClient.SendMessage(MessageType.Update, newProduct);
            return newProduct;
        }
    }
}