using System.Collections.Generic;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class EditRightsViewModel
    {
        public string UserName { get; set; }

        public List<RoleViewModel> UserRoles { get; set; } //список ролей которые есть у пользователя
        public List<RoleViewModel> AllRoles { get; set; } //Список всех ролей которые есть в системе
    }
}
