using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OnlineShopWebApp
{
    public class UsersManager : IUsersManager
    {
        private readonly List<UserViewModel> users = new List<UserViewModel>();
        public List<UserViewModel> GetAll()
        {
            return users;
        }
        public void Add(UserViewModel user)
        {
            users.Add(user);
        }
        public UserViewModel TryGetByName(string name)
        {
            return users.FirstOrDefault(x => x.Name == name);
        }

        public void ChangePassword(string userName, string newPassword)
        {
           var account = TryGetByName(userName);
            //account.Password = newPassword;
        }
    }
}
