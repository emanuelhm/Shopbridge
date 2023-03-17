using Microsoft.EntityFrameworkCore;
using ShopBridge.Domain.Models;

namespace ShopBridge.Data
{
    public class Shopbridge_Context : DbContext
    {
        public Shopbridge_Context(DbContextOptions<Shopbridge_Context> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var category = new Category()
            {
                Category_Id = 1,
                Name = "Category 01",
            };

            modelBuilder.Entity<Category>().HasData(category);

            var tag = new Tag()
            {
                Tag_Id = 1,
                Name = "Tag 01",
            };

            modelBuilder.Entity<Tag>().HasData(tag);

            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Product_Id = 1,
                Name = "Product 01",
                Description = "Product 01",
                Price = 1,
            });

            modelBuilder.Entity("ProductTag").HasData(new { ProductsProduct_Id = 1, TagsTag_Id = 1 });
            modelBuilder.Entity("CategoryProduct").HasData(new { ProductsProduct_Id = 1, CategoriesCategory_Id = 1 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
