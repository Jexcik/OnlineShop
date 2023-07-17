using OnlineShopWebApp.Areas.Admin.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public interface IRolesRepository
	{
        List<RoleViewModel> GetAll();
        RoleViewModel TryGetByName(string Name);
        void Add(RoleViewModel role);
        void Remove(string name);
    }
}