using Microsoft.AspNetCore.Identity;
using PsychoShop.Framework.Application;

namespace PsychoShop.Application.Contracts.UserAccount
{
    public interface IUserAccountApplication
    {
        Task<SignInResult> SignIn(SignInUserAccount command);
        Task<IdentityResult> SignUp(SignUpUserAccount command);
        Task<IdentityResult> Edit(EditUserAccount command);
        Task<IdentityResult> ResetPassword(ResetPassword command);
        Task<OperationResult> ForgotPassword(ForgotPassword command);
        Task<OperationResult> ConfirmEmail(string userName, string token);
        Task<OperationResult> SendEmailConfirmation();
        Task<EditUserAccount> GetDetails(string id);
        Task<UserAccountViewModel> GetCurrentUserAccountInfo();
        Task<List<UserAccountViewModel>> Search(UserAccountSearchModel searchModel);
        Task SignOut();
    }
}
