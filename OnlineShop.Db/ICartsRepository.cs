using OnlineShop.Db.Models;
using System;

namespace OnlineShop.Db
{
	public interface ICartsRepository
	{
		void Add(Product product, string userId);
        Cart TruGetByUserId(string userId);
        void DecreaseAmount(Guid productId, string userId);
        void Clear(string userId);
    }
}