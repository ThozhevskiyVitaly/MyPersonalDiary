using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyPersonalDiary.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}