using System.Linq;
using System.Collections.Generic;
using OnlineShop.Db.Models;
using System;

namespace OnlineShop.Db
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        //private List<Product> products = new List<Product>()
        //{
        //    new Product("Revit",200,"Desc1","/images/Revit.jpg"),
        //    new Product("Tekla",500,"Desc2","/images/Tekla.png"),
        //    new Product("Renga",100,"Desc3","/images/Renga.jpg"),
        //    new Product("AllPlan",500,"Desc4","/images/AllPlan.jpg"),
        //};

        public void Add(Product product)
        {
            //product.ImagePath = "/images/Renga.jpg";
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return databaseContext.Products.ToList();
        }
        public Product TryGetById(Guid id)
        {
            return databaseContext.Products.FirstOrDefault(product => product.Id == id); //То-же самое что и foreach

            //foreach (var product in products)
            //{
            //    if (product.Id == id)
            //    {
            //        return product;
            //    }
            //}
            //return null;
        }

        public void Update(Product product)
        {
            var existingProduct= databaseContext.Products.FirstOrDefault(x=>x.Id==product.Id);
            if(existingProduct==null)
            {
                return;
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Cost = product.Cost;
            databaseContext.SaveChanges();
        }
    }
}
