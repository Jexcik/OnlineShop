using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class FavoriteDbRepository : IFavoriteRepository
    {
        private readonly DatabaseContext databaseContext;

        public FavoriteDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public void Add(string userId, Product product )
        {
            var existingProduct = databaseContext.FavoriteProducts.FirstOrDefault( x => x.UserId == userId && x.Product.Id == product.Id );//Проверка на то есть ли такой продукт
            if (existingProduct != null) //Если продукта нет то добавляем его
            {
                databaseContext.FavoriteProducts.Add(new FavoriteProduct { Product = product, UserId = userId});//Добавляем в базу данных связку продукта и пользователя
                databaseContext.SaveChanges();
            }
        }
        public void Clear(string userId)
        {
            var userFavoriteProducts = databaseContext.FavoriteProducts.Where( u => u.UserId == userId ).ToList();//Находим избранные товары у конкретного пользователя
            databaseContext.FavoriteProducts.RemoveRange( userFavoriteProducts );//Из таблицы FavoriteProducts удаляем связки пользователя с избранными продуктами
            databaseContext.SaveChanges(); 
        }
        public List<Product> GetAll(string userId) //Получаем все продукты которые были в избранном у текущего пользователя
        {
            return databaseContext.FavoriteProducts.Where(x => x.UserId == userId) //Фильтруем все те продукты у которых 
                .Include(x =>x.Product) //Подключаем информацию по продукту
                .Select(x => x.Product) //Взять список продуктов
                .ToList();
        }
        public void Remove(string userId, Guid productId) //Удаляем конкретный продукт по Guid и userId
        {
            var removingFavorite = databaseContext.FavoriteProducts.FirstOrDefault(u =>u.UserId == userId && u.Product.Id == productId);
            databaseContext.FavoriteProducts.Remove(removingFavorite); //Находим продукт и пытаемся удалить
            databaseContext.SaveChanges();
        }
    }
}
