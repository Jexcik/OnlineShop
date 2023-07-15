using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
