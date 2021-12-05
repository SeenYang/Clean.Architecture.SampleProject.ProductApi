using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampleProject.Domain.Models;

namespace SampleProject.Application
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProductsByName(string name);
        Task<ProductModel> GetProductBySku(string sku);
        Task<ProductModel> GetProductById(Guid sku);
        Task<ProductModel> AddProduct(ProductModel product);
        Task<ProductModel> UpdateProduct(ProductModel product);
    }
}