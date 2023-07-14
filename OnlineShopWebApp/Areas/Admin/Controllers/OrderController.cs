using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        public OrderController(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }
        public IActionResult Index()
        {
            var orders = ordersRepository.GetAll();
            return View(orders.Select(x=> Mapping.ToOrderViewModel(x)).ToList());
        }
        public IActionResult Detail(Guid orderId)
        {
            var order = ordersRepository.TryGetById(orderId);
            return View(Mapping.ToOrderViewModel(order));
        }
        public IActionResult UpdateOrderStatus(Guid orderId, OrderStatusViewModel status)
        {
            ordersRepository.UpdateStatus(orderId, (OrderStatus)(int)status);
            return RedirectToAction(nameof(Index));
        }
    }
}
