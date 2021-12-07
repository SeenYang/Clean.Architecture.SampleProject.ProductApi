using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Infra.Models.Entity;

namespace SampleProject.Infra.Interface
{
    public interface IProductDbContext: IDisposable
    {
        public DbSet<ProductEntity> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}