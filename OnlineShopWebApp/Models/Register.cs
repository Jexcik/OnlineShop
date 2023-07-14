using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Register
    {
        [Required(ErrorMessage ="Любая фигня")]
        [EmailAddress(ErrorMessage = "Введите валидный E-mail")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Не указан телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(100, MinimumLength = 8, ErrorMessage ="Пароль должен содержать от 8 до 100 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Поле обязательно для заполнения")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Пароль должен содержать от 8 до 100 символов")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")] //Атрибут для сравнения паролей
        public string ConfirmPassword { get; set; }
    }
}
