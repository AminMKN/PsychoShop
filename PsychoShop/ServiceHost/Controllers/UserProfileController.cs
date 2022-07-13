using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.UserAccount;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Application.AuthHelper;
using ServiceHost.Models;

namespace ServiceHost.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IAuthHelper _authHelper;
        private readonly IUserAccountApplication _userAccountApplication;

        public UserProfileController(IUserAccountApplication userAccountApplication, IAuthHelper authHelper)
        {
            _authHelper = authHelper;
            _userAccountApplication = userAccountApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var command = new UserProfileCommand()
            {
                UserAccount = await _userAccountApplication.GetCurrentUserAccountInfo()
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserProfileCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAccountApplication.SendEmailConfirmation();
                command.Message = result.Message;
            }

            command.UserAccount = await _userAccountApplication.GetCurrentUserAccountInfo();
            return View(command);
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var command = new UserProfileCommand()
            {
                EditUserAccount = await _userAccountApplication.GetDetails(_authHelper.GetCurrentUserAccountId())
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfileCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAccountApplication.Edit(command.EditUserAccount);
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

        #region ConfirmEmail

        public async Task<IActionResult> ConfirmEmail(string userName, string token)
        {
            var result = await _userAccountApplication.ConfirmEmail(userName, token);
            var command = new UserProfileCommand()
            {
                Message = result.Message,
                UserAccount = await _userAccountApplication.GetCurrentUserAccountInfo()
            };

            return View("Index", command);
        }

        #endregion

    }
}
