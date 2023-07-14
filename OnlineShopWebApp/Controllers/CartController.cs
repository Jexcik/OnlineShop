using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShopWebApp.Controllers
{
    [Authorize] //Не пускает не авторизованного пользователя выполнять методы
    public class CartController : Controller
    {
        private readonly IProductsRepository productRepository;
		private readonly ICartsRepository cartsRepository;


		public CartController(IProductsRepository productRepository, ICartsRepository cartsRepository)
		{
			this.productRepository = productRepository;
			this.cartsRepository = cartsRepository;
		}

		public IActionResult Index()
        {
           var cart = cartsRepository.TruGetByUserId(Constants.UserId);

            return View(Mapping.ToCartViewModel(cart));
        }

        public IActionResult Add(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
			cartsRepository.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult DecreaseAmount(Guid productId)
        {
            cartsRepository.DecreaseAmount(productId, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Clear ()
        {
            cartsRepository.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
