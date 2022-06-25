﻿using PsychoShop.Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace PsychoShop.Application.Contracts.Account
{
    public class ForgotPassword
    {
        [EmailAddress(ErrorMessage = ValidationMessages.EmailIsNotValid)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Email { get; set; }
    }
}
