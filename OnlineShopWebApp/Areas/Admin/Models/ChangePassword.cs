using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class ChangePassword
    {
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
