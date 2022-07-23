using PsychoShop.Application.Contracts.Order;
using PsychoShop.Application.Contracts.UserAccount;

namespace ServiceHost.Models
{
    public class UserProfileCommand
    {
        public string Message { get; set; }
        public EditUserAccount EditUserAccount { get; set; }
        public UserAccountViewModel UserAccount { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}
