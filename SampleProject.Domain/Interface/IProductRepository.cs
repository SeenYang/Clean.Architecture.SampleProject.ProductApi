using System;
using System.Collections.Generic;

namespace SampleProject.Domain
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> GetProductByName(string productName);
        ProductModel GetProductById(Guid id);
        ProductModel GetProductBySku(string sku);
    }
}