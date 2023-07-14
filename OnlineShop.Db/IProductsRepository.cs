using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
	public interface IProductsRepository
	{
        List<Product> GetAll();
		Product TryGetById(Guid id);
        void Add(Product product);
        void Update(Product product);
    }
}