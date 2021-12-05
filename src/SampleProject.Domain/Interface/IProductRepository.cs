using System;
using System.Collections.Generic;
using SampleProject.Domain.Models;

namespace SampleProject.Domain.Interface
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> GetProductByName(string productName);
        ProductModel GetProductById(Guid id);
        ProductModel GetProductBySku(string sku);
    }
}