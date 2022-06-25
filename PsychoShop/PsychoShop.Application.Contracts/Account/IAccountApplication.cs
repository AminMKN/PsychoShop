using PsychoShop.Framework.Application;

namespace PsychoShop.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult SignUp(SignUpAccount command);
        OperationResult SignIn(SignInAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult ConfirmEmail(string userName, string token);
        Task<OperationResult> ForgotPassword(ForgotPassword command);
        Task<OperationResult> SendEmailConfirmation();
        EditAccount GetDetails(int id);
        Task<AccountViewModel> GetCurrentAccountInfo();
        Task<List<AccountViewModel>> GetAccountsList();
        Task<List<AccountViewModel>> Search(AccountSearchModel searchModel);
        void SignOut();
    }
}
