using PsychoShop.Application.Contracts.Account;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.AccountAgg
{
    public interface IAccountRepository : IRepositoryBase<int, Account>
    {
        Account GetAccountByUserName(string userName);
        Account GetAccountByEmail(string email);
        EditAccount GetDetails(int id);
        Task<AccountViewModel> GetCurrentAccountInfo();
        Task<List<AccountViewModel>> GetAccountsList();
        Task<List<AccountViewModel>> Search(AccountSearchModel searchModel);
    }
}
