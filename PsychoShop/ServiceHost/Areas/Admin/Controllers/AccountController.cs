using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.Account;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class AccountController : Controller
    {
        private readonly IAccountApplication _accountApplication;

        public AccountController(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(AccountSearchModel searchModel)
        {
            var account = new AccountAdminModel()
            {
                Accounts = await _accountApplication.Search(searchModel)
            };

            return View(account);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AccountAdminModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AccountAdminModel command)
        {
            if (ModelState.IsValid)
            {
                var result = _accountApplication.SignUp(command.SignUpAccount);
                command.Message = result.Message;
            }

            return View(command);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var account = new AccountAdminModel()
            {
                EditAccount = _accountApplication.GetDetails(id)
            };

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AccountAdminModel command)
        {
            if (ModelState.IsValid)
            {
                var result = _accountApplication.Edit(command.EditAccount);
                command.Message = result.Message;
            }

            return View(command);
        }

        #endregion

    }
}
