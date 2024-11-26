using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SovosProjectTest.Domain.Entities;

namespace SovosProjectTest.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings => warnings
                .Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.NewGuid(),  Name = "Product A", Category = "Category 1", Price = 10.0M, StockQuantity = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Product B", Category = "Category 2", Price = 20.0M , StockQuantity = 10},
                new Product { Id = Guid.NewGuid(), Name = "Product C", Category = "Category 1", Price = 30.0M , StockQuantity = 10}
            );
        }
    }
}
