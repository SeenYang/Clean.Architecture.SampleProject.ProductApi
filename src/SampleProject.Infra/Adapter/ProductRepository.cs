using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SampleProject.Domain.Interface;
using SampleProject.Domain.Models;
using SampleProject.Infra.Interface;

namespace SampleProject.Infra.Adapter
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductDbContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ILogger<ProductRepository> logger, IProductDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<IEnumerable<ProductModel>> GetProductsByName(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetProductById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetProductBySku(string sku)
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