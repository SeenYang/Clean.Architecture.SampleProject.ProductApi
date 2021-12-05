using System;

namespace SampleProject.Domain.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public ProductType ProductType { get; set; }
        public string Sku { get; set; } = null!;
        public string Description { get; set; } = "";
    }
}