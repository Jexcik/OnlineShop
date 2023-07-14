using OnlineShopWebApp.Areas.Admin.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public interface IRolesRepository
	{
        List<Role> GetAll();
        Role TryGetByName(string Name);
        void Add(Role role);
        void Remove(string name);
    }
}