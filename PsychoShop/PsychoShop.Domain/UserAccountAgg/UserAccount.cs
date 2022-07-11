using Microsoft.AspNetCore.Identity;

namespace PsychoShop.Domain.UserAccountAgg
{
    public class UserAccount : IdentityUser
    {
        public string FullName { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
