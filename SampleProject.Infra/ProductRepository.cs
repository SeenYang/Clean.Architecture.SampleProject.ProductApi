using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SampleProject.Domain.Interface;
using SampleProject.Domain.Models;
using SampleProject.Infra.Interface;

namespace SampleProject.Infra
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IProductDbContext _dbContext;

        public ProductRepository(ILogger<ProductRepository> logger, IProductDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IEnumerable<ProductModel> GetProductByName(string productName)
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProductById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProductBySku(string sku)
        {
            throw new NotImplementedException();
        }
    }
}