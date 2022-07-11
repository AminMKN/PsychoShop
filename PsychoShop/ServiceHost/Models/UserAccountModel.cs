using PsychoShop.Application.Contracts.UserAccount;

namespace ServiceHost.Models
{
    public class UserAccountModel
    {
        public string Message { get; set; }
        public ResetPassword ResetPassword { get; set; }
        public ForgotPassword ForgotPassword { get; set; }
        public SignInUserAccount SignInUserAccount { get; set; }
        public SignUpUserAccount SignUpUserAccount { get; set; }
    }
}
