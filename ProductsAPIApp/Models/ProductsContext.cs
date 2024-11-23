using Microsoft.EntityFrameworkCore;

namespace ProductsAPIApp.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product() {ProductId = 1,ProductName = "Iphone14",Price = 60000,IsActive = true});
            modelBuilder.Entity<Product>().HasData(new Product() {ProductId = 2,ProductName = "Iphone15",Price = 70000,IsActive = true});
            modelBuilder.Entity<Product>().HasData(new Product() {ProductId = 3,ProductName = "Iphone16",Price = 80000,IsActive = true});
            
        }
        public DbSet<Product> Products { get; set; }
    }
}