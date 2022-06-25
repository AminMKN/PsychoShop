using PsychoShop.Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace PsychoShop.Application.Contracts.Account
{
    public class SignInAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
