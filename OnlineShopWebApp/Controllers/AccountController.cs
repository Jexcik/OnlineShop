using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersManager usersManager;

        public AccountController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Login));

            var userAccount = usersManager.TryGetByName(login.UserName);
            if (userAccount == null) //Если нет аккаунта то кидаем ошибку на уровне модели
            {
                ModelState.AddModelError("", "Такого пользователя не существует");
                return RedirectToAction(nameof(Login));
            }

            if (userAccount.Password != login.Password) //Если не правильный логин и пароль
            {
                ModelState.AddModelError("", "Не правильный пароль");//Кидаем вот такую ошибку
                return RedirectToAction(nameof(Login));
            }
            //Если дошли до сюда то отправляем на HomeController
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(Register register)
        {
            if (register.UserName == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");
            }

            if (ModelState.IsValid)
            {
                usersManager.Add(new UserAccount
                {
                    Name = register.UserName,
                    Password = register.Password,
                    Phone = register.Phone
                });
                const string a = nameof(HomeController.Index);
                return RedirectToAction(a, "Home");
            }
            return RedirectToAction(nameof(Register));
        }
    }
}
