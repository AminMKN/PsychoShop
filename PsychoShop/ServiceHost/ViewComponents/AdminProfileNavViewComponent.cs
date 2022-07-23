using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.UserAccount;
using PsychoShop.Domain.UserAccountAgg;

namespace ServiceHost.ViewComponents
{
    public class AdminProfileNavViewComponent : ViewComponent
    {
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly IUserAccountApplication _userAccountApplication;

        public AdminProfileNavViewComponent(IUserAccountApplication userAccountApplication, SignInManager<UserAccount> signInManager)
        {
            _userAccountApplication = userAccountApplication;
            _signInManager = signInManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn((System.Security.Claims.ClaimsPrincipal)User))
            {
                var userAccount = await _userAccountApplication.GetCurrentUserAccountInfo();
                return View(userAccount);
            }

            return View(new UserAccountViewModel());
        }
    }
}
