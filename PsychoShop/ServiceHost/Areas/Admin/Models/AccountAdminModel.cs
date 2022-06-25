using PsychoShop.Application.Contracts.Account;

namespace ServiceHost.Areas.Admin.Models
{
    public class AccountAdminModel
    {
        public string Message { get; set; }
        public EditAccount EditAccount { get; set; }
        public SignUpAccount SignUpAccount { get; set; }
        public AccountSearchModel SearchModel { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
    }
}
