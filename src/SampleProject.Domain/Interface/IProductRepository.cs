using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetProductsByName(string productName);
        Task<ProductModel> GetProductById(Guid id);
        Task<ProductModel> GetProductBySku(string sku);
        Task<ProductModel> AddProduct(ProductModel product);
        Task<ProductModel> UpdateProduct(ProductModel product);
    }
}