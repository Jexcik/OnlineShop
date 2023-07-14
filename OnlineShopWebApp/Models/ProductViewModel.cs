using System.ComponentModel.DataAnnotations;
using System;

namespace OnlineShopWebApp.Models
{
    public class ProductViewModel
    {
        private static int instanceCounter = 0;

        public Guid Id { get; set; }
        public string Name { get; set; }

        [Range(1,1000,ErrorMessage ="Цена должна быть в пределах от 1 до 1 000 руб.")]
        public decimal Cost { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }
    }
}
