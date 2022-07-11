using PsychoShop.Application.Contracts.UserAccount;

namespace PsychoShop.Domain.UserAccountAgg
{
    public interface IUserAccountRepository
    {
        Task<EditUserAccount> GetDetails(string id);
        Task<UserAccountViewModel> GetCurrentUserAccountInfo();
        Task<List<UserAccountViewModel>> Search(UserAccountSearchModel searchModel);
    }
}
