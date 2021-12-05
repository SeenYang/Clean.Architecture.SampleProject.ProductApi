using Microsoft.EntityFrameworkCore;
using SampleProject.Infra.Entity;
using SampleProject.Infra.Interface;

namespace SampleProject.Infra
{
    public class ProductDbContext : DbContext, IProductDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        
        
        public DbSet<ProductEntity> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().ToTable("Product");
        }
    }
}