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

        public Task<IEnumerable<ProductModel>> GetProductsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetProductBySku(string sku)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetProductById(Guid sku)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> AddProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> UpdateProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}