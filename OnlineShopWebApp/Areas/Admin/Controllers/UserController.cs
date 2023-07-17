using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Linq;



namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)] //Говорит о том что должен быть авторизованный доступ
    [Authorize(Roles = Constants.AdminRoleName)] //Вот с такими ролями
    public class UserController : Controller
    {
        private readonly UserManager<User> usersManager;

        public UserController(UserManager<User> usersManager)
        {
            this.usersManager = usersManager;
        }
        public IActionResult Index()
        {
            var users = usersManager.Users.ToList();
            return View(users.Select(x => x.ToUserViewModel()).ToList());
        }
        public IActionResult Detail(string name)
        {
            var user = usersManager.FindByNameAsync(name).Result;
            return View(user.ToUserViewModel());
        }
        public IActionResult ChangePassword(string name)
        {
            var changePassword = new ChangePassword()
            {
                UserName = name
            };
            return View(changePassword);
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if (changePassword.UserName == changePassword.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");
            }
            if (ModelState.IsValid)
            {
                var user = usersManager.FindByNameAsync(changePassword.UserName).Result;
                //Перенести в личный кабинет пользователя
                var newHashPassword = usersManager.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newHashPassword;
                usersManager.UpdateAsync(user).Wait();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(ChangePassword));
        }
    }
}
