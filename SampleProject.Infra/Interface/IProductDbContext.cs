using Microsoft.EntityFrameworkCore;
using SampleProject.Infra.Entity;

namespace SampleProject.Infra.Interface
{
    public interface IProductDbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
    }
}