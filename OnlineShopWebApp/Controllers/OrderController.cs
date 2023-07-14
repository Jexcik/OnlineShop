using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IOrdersRepository ordersRepository;

        public OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository)
        {
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buy(UserDeliveryInfoViewModel userViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Index", userViewModel);
            }
            var existingCart = cartsRepository.TruGetByUserId(Constants.UserId);

            var order = new Order
            {
                User = Mapping.ToUser(userViewModel),
                Items = existingCart.Items
            };
            ordersRepository.Add(order);
            cartsRepository.Clear(Constants.UserId);
            return View();
        }
    }
}
