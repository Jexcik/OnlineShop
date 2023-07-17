using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Admin.Models;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)] //Говорит о том что должен быть авторизованный доступ
    [Authorize(Roles = Constants.AdminRoleName)] //Вот с такими ролями
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> rolesManager;

        public RoleController(RoleManager<IdentityRole> rolesManager)
        {
            this.rolesManager = rolesManager;
        }

        public IActionResult Index()
        {
            var roles = rolesManager.Roles.ToList();
            return View(roles.Select(x=>new RoleViewModel { Name=x.Name}).ToList());
        }

        public IActionResult Remove(string roleName)
        {
            var role = rolesManager.FindByNameAsync(roleName).Result; //Ищим роль
            if(role !=null)
            {
                rolesManager.DeleteAsync(role).Wait();//Если она есть мы ее удаляем
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Add()
        {
            return View();
        }
        //Добавляем роль в модель
        [HttpPost]
        public IActionResult Add(RoleViewModel role)
        {
            var result = rolesManager.CreateAsync(new IdentityRole(role.Name)).Result;
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var error in result.Errors) 
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(role);
        }

    }
}
