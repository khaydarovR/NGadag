using System.ComponentModel.DataAnnotations;

namespace RRshop.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(250, ErrorMessage = "Превышена допустимая длина")]
        [MinLength(8, ErrorMessage = "Мало символов")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Не корректный номер телефона")]
        [Compare("Phone", ErrorMessage = "Не корректная почта")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(250, ErrorMessage = "Превышена допустимая длина")]
        [MinLength(4, ErrorMessage = "Мало символов")]
        [DataType(DataType.Password, ErrorMessage = "Не корректный пароль")]
        public string? Password { get; set; }
    }
}
