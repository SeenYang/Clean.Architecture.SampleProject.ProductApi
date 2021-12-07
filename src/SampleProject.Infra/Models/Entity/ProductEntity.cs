using System;
using System.ComponentModel.DataAnnotations;
using SampleProject.Domain.Models;

namespace SampleProject.Infra.Models.Entity
{
    public class ProductEntity
    {
        [Key] public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public ProductType ProductType { get; set; }
        public string Sku { get; set; } = null!;
        public string Description { get; set; } = "";
    }
}