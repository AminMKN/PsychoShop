using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.UserAccount;
using PsychoShop.Application.Contracts.UserClaim;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize(Policy = "UserAccountPolicy")]
    public class UserAccountController : Controller
    {
        private readonly IUserClaimApplication _userClaimApplication;
        private readonly IUserAccountApplication _userAccountApplication;

        public UserAccountController(IUserAccountApplication userAccountApplication, IUserClaimApplication userClaimApplication)
        {
            _userClaimApplication = userClaimApplication;
            _userAccountApplication = userAccountApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(UserAccountSearchModel searchModel)
        {
            var command = new UserAccountAdminModel()
            {
                UserAccounts = await _userAccountApplication.Search(searchModel)
            };

            return View(command);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View(new UserAccountAdminModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserAccountAdminModel command)
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

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var command = new UserAccountAdminModel()
            {
                EditUserAccount = await _userAccountApplication.GetDetails(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserAccountAdminModel command)
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

        #region AddUserClaim

        [HttpGet]
        public async Task<IActionResult> AddUserClaim(string id)
        {
            var command = new UserAccountAdminModel()
            {
                AddOrRemoveClaim = await _userClaimApplication.GetDetailsForAdd(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserClaim(UserAccountAdminModel command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userClaimApplication.Add(command.AddOrRemoveClaim);
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

        #region RemoveUserClaim

        [HttpGet]
        public async Task<IActionResult> RemoveUserClaim(string id)
        {
            var command = new UserAccountAdminModel()
            {
                AddOrRemoveClaim = await _userClaimApplication.GetDetailsForRemove(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserClaim(UserAccountAdminModel command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userClaimApplication.Remove(command.AddOrRemoveClaim);
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

    }
}
