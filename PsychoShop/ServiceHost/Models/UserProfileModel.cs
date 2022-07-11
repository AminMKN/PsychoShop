using PsychoShop.Application.Contracts.UserAccount;

namespace ServiceHost.Models
{
    public class UserProfileModel
    {
        public string Message { get; set; }
        public EditUserAccount EditUserAccount { get; set; }
        public UserAccountViewModel UserAccount { get; set; }
    }
}
