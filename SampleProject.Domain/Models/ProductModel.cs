using System;

namespace SampleProject.Domain
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public ProductType ProductType { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
    }
}