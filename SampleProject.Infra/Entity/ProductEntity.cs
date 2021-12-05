using System;
using SampleProject.Domain.Models;

namespace SampleProject.Infra.Entity
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public ProductType ProductType { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
    }
}