using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsRepository productsRepository;

        public ProductController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(Mapping.ToProductViewModels(products));
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var productDb = new Product
            {
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description
            };

            productsRepository.Add(productDb);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var productDb = new Product
            {
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description
            };

            productsRepository.Update(productDb);
            return RedirectToAction(nameof(Index));
        }


    }
}
