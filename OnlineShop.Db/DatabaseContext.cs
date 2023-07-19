using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        //Доступ к таблицам
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            //Database.EnsureCreated(); //Применяем миграцию
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().HasOne(p=> p.Product).WithMany(p=>p.Images).HasForeignKey(p=>p.ProductId).OnDelete(DeleteBehavior.Cascade);

            var product1Id = Guid.Parse("92bced76-82ba-4f44-af74-70eb7b31a6f9");
            var product2Id = Guid.Parse("ba7aec10-45d0-49ad-8ee6-ddbe95371796");
            var image1 = new Image
            {
                Id = Guid.Parse("c96dc613-1372-4746-87d7-47fed78a990b"),
                Url = "/images/Products/image1.jpg",
                ProductId = product1Id
            };
        }
    }
}

