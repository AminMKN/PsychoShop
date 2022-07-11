using PsychoShop.Application.Contracts.UserAccount;
using PsychoShop.Application.Contracts.UserClaim;

namespace ServiceHost.Areas.Admin.Models
{
    public class UserAccountAdminModel
    {
        public string Message { get; set; }
        public AddOrRemoveClaim AddOrRemoveClaim { get; set; }
        public EditUserAccount EditUserAccount { get; set; }
        public SignUpUserAccount SignUpUserAccount { get; set; }
        public UserAccountSearchModel SearchModel { get; set; }
        public List<UserAccountViewModel> UserAccounts { get; set; }
    }
}
