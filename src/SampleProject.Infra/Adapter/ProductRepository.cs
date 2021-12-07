using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleProject.Domain.Interface;
using SampleProject.Domain.Models;
using SampleProject.Infra.Entity;
using SampleProject.Infra.Interface;

namespace SampleProject.Infra.Adapter
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductDbContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMapper _mapper;

        public ProductRepository(ILogger<ProductRepository> logger, IProductDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByName(string productName)
        {
            var entities = await _dbContext.Products.Where(p => string.Equals(p.Name, productName)).ToListAsync();
            return _mapper.Map<List<ProductEntity>, List<ProductModel>>(entities);
        }

        public async Task<ProductModel> GetProductById(Guid id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProductEntity, ProductModel>(entity);
        }

        public async Task<ProductModel> GetProductBySku(string sku)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => string.Equals(p.Sku, sku));
            return _mapper.Map<ProductEntity, ProductModel>(entity);
        }

        public async Task<ProductModel> AddProduct(ProductModel product)
        {
            var entity = _mapper.Map<ProductModel, ProductEntity>(product);
            try
            {
                var newProduct = await _dbContext.Products.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<ProductEntity, ProductModel>(newProduct.Entity);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to Add new product. Exception: {Exp}", e.Message);
                throw;
            }
        }

        public async Task<ProductModel> UpdateProduct(ProductModel product)
        {
            if (!await _dbContext.Products.AnyAsync(p => p.Id == product.Id))
            {
                _logger.LogError("Product not found. Id: {Id}", product.Id);
            }

            var entity = _mapper.Map<ProductModel, ProductEntity>(product);
            try
            {
                var newProduct = _dbContext.Products.Update(entity);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<ProductEntity, ProductModel>(newProduct.Entity);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update new product. Exception: {Exp}", e.Message);
                throw;
            }
        }
    }
}