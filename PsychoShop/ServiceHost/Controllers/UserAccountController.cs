using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.UserAccount;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Framework.Application;
using ServiceHost.Models;

namespace ServiceHost.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly IUserAccountApplication _userAccountApplication;

        public UserAccountController(IUserAccountApplication userAccountApplication, SignInManager<UserAccount> signInManager)
        {
            _userAccountApplication = userAccountApplication;
            _signInManager = signInManager;
        }

        #region SignUp

        [HttpGet]
        public IActionResult SignUp()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            return View(new UserAccountModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserAccountModel command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAccountApplication.SignUp(command.SignUpUserAccount);
                if (result.Succeeded)
                    command.Message = ApplicationMessages.TaskSuccessful;

                foreach (var error in result.Errors)
                {
                    command.Message = error.Description;
                }
            }

            return View(command);
        }

        #endregion

        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            return View(new UserAccountModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserAccountModel command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAccountApplication.SignIn(command.SignInUserAccount);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");

                command.Message = ApplicationMessages.UserNameOrPasswordNotValid;
            }

            return View(command);
        }

        #endregion

        #region SignOut

        public async Task<IActionResult> SignOut()
        {
            await _userAccountApplication.SignOut();
            return RedirectToAction("SignIn", "UserAccount");
        }

        #endregion

        #region ForgotPassword

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new UserAccountModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(UserAccountModel command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAccountApplication.ForgotPassword(command.ForgotPassword);
                command.Message = result.Message;
            }

            return View(command);
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            var command = new UserAccountModel()
            {
                ResetPassword = new ResetPassword()
                {
                    Email = email,
                    Token = token
                }
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(UserAccountModel command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAccountApplication.ResetPassword(command.ResetPassword);
                if (result.Succeeded)
                    command.Message = ApplicationMessages.ResetPasswordSuccessful;

                foreach (var error in result.Errors)
                {
                    command.Message = error.Description;
                }
            }

            return View(command);
        }

        #endregion

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
