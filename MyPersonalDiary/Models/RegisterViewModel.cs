using System.ComponentModel.DataAnnotations;

namespace MyPersonalDiary.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Min length of name 3 characters")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50,MinimumLength =8,ErrorMessage ="Min length of password 8 characters")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}