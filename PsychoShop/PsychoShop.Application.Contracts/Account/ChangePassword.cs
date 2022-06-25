using PsychoShop.Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace PsychoShop.Application.Contracts.Account
{
    public class ChangePassword
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MinLength(6, ErrorMessage = ValidationMessages.MinPasswordLength)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Compare(nameof(Password), ErrorMessage = ValidationMessages.PasswordAndRePasswordDoNotMatch)]
        public string RePassword { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Token { get; set; }
    }
}
