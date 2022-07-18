using PsychoShop.Application.Contracts.UserAccount;

namespace PsychoShop.Domain.UserAccountAgg
{
    public interface IUserAccountRepository
    {
        Task<string> GetUserName(string userAccountId);
        Task<EditUserAccount> GetDetails(string id);
        Task<UserAccountViewModel> GetCurrentUserAccountInfo();
        Task<List<UserAccountViewModel>> Search(UserAccountSearchModel searchModel);
    }
}
