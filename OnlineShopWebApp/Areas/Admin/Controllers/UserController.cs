using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUsersManager usersManager;

        public UserController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }
        public IActionResult Index()
        {
            var userAccounts = usersManager.GetAll();
            return View(userAccounts);
        }
        public IActionResult Detail(string name)
        {
            var userAccounts = usersManager.TryGetByName(name);
            return View(userAccounts);
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
            if(changePassword.UserName == changePassword.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");
            }
            if(ModelState.IsValid)
            {
                usersManager.ChangePassword(changePassword.UserName, changePassword.Password);
                    return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(ChangePassword));
        }
    }
}
