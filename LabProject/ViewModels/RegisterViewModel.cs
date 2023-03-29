using System.ComponentModel.DataAnnotations;

namespace LabProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name="Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "ПІБ")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Підтвердити пароль")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
