using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.Account;
using PsychoShop.Domain.AccountAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Application.AuthHelper;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<int, Account>, IAccountRepository
    {
        private readonly IAuthHelper _authHelper;
        private readonly PsychoShopContext _context;

        public AccountRepository(PsychoShopContext context, IAuthHelper authHelper) : base(context)
        {
            _context = context;
            _authHelper = authHelper;
        }

        public Account GetAccountByUserName(string userName)
        {
            return _context.Accounts.FirstOrDefault(x => x.UserName == userName);
        }

        public Account GetAccountByEmail(string email)
        {
            return _context.Accounts.FirstOrDefault(x => x.Email == email);
        }

        public EditAccount GetDetails(int id)
        {
            return _context.Accounts.Select(x => new EditAccount()
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                FullName = x.FullName,
                MobilePhone = x.MobilePhone
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<AccountViewModel> GetCurrentAccountInfo()
        {
            return await _context.Accounts.Select(x => new AccountViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                FullName = x.FullName,
                MobilePhone = x.MobilePhone,
                ProfilePhoto = x.ProfilePhoto,
                EmailConfirmed = x.EmailConfirmed,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == _authHelper.GetCurrentAccountId());
        }

        public async Task<List<AccountViewModel>> GetAccountsList()
        {
            return await _context.Accounts.Select(x => new AccountViewModel()
            {
                Id = x.Id,
                UserName = x.UserName
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<AccountViewModel>> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Select(x => new AccountViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                FullName = x.FullName,
                MobilePhone = x.MobilePhone,
                ProfilePhoto = x.ProfilePhoto,
                EmailConfirmed = x.EmailConfirmed,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
