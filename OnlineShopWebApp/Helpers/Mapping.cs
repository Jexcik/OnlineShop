using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static List<ProductViewModel> ToProductViewModels(this List<Product> products)
        {
            var productsViewModels = new List<ProductViewModel>();
            foreach (var product in products)
            {
                productsViewModels.Add(ToProductViewModel(product));
            }

            return productsViewModels;
        }

        public static ProductViewModel ToProductViewModel(this Product product)
        {

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImagePath = product.ImagePath,
            };
        }

        public static OrderViewModel ToOrderViewModel(this Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                CreateDateTime = order.CreateDateTime,
                Status = (OrderStatusViewModel)(int)order.Status,
                User = ToUserDeliveryInfoViewModel(order.User),
                Items = ToCartItemViewModels(order.Items)
            };
        }

        public static UserDeliveryInfoViewModel ToUserDeliveryInfoViewModel(this UserDeliveryInfo deliveryInfo)
        {
            return new UserDeliveryInfoViewModel
            {
                Name = deliveryInfo.Name,
                Address = deliveryInfo.Address,
                Phone = deliveryInfo.Phone
            };
        }

        public static Product ToProduct(this ProductViewModel productViewModel)
        {
            return new Product
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Cost = productViewModel.Cost,
                Description = productViewModel.Description,
                ImagePath = productViewModel.ImagePath,
            };
        }

        public static UserDeliveryInfo ToUser(this UserDeliveryInfoViewModel user)
        {
            return new UserDeliveryInfo
            {
                Name = user.Name,
                Address = user.Address,
                Phone = user.Phone,
            };
        }

        public static CartViewModel ToCartViewModel(this Cart cart)
        {
            if (cart == null)
            {
                return null;
            }
            return new CartViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = ToCartItemViewModels(cart.Items)
            };
        }

        public static List<CartItemViewModel> ToCartItemViewModels(this List<CartItem> cartDbItems)
        {
            var cartItems = new List<CartItemViewModel>();
            foreach (var cartDbItem in cartDbItems)
            {
                var cartItem = new CartItemViewModel
                {
                    Id = cartDbItem.Id,
                    Amount = cartDbItem.Amount,
                    Product = ToProductViewModel(cartDbItem.Product)
                };
                cartItems.Add(cartItem);
            }
            return cartItems;
        }
        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Name = user.UserName,
                Phone = user.PhoneNumber
            };
        }
    }
}
