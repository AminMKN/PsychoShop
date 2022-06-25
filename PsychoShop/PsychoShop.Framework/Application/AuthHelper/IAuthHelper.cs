namespace PsychoShop.Framework.Application.AuthHelper
{
    public interface IAuthHelper
    {
        int GetCurrentAccountId();
        string GetCurrentAccountFullName();
        string GetCurrentAccountUserName();
        string GetCurrentAccountEmail();
        string GetCurrentAccountMobilePhone();
        void SignIn(AuthViewModel command);
        void SignOut();
        bool IsAuthenticated();
    }
}
