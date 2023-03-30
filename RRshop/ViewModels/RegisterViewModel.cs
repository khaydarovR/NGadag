using System.ComponentModel.DataAnnotations;

namespace RRshop.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(250, ErrorMessage = "Превышена допустимая длина")]
        [MinLength(8, ErrorMessage = "Мало символов")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Не корректный номер телефона")]
        [Compare("Phone", ErrorMessage = "Не корректная почта")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(250, ErrorMessage = "Превышена допустимая длина")]
        [MinLength(3, ErrorMessage = "Мало символов")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(250, ErrorMessage = "Превышена допустимая длина")]
        [MinLength(3, ErrorMessage = "Мало символов")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(250, ErrorMessage = "Превышена допустимая длина")]
        [MinLength(4, ErrorMessage = "Мало символов")]
        [DataType(DataType.Password, ErrorMessage = "Не корректный пароль")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Повторите пороль правильно")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}
