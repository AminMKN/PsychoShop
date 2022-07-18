using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.UserAccount;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Framework.Application.AuthHelper;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IAuthHelper _authHelper;
        private readonly UserManager<UserAccount> _userManager;

        public UserAccountRepository(IAuthHelper authHelper, UserManager<UserAccount> userManager)
        {
            _authHelper = authHelper;
            _userManager = userManager;
        }

        public async Task<string> GetUserName(string userAccountId)
        {
            var userAccount = await _userManager.FindByIdAsync(userAccountId);
            return userAccount.UserName;
        }

        public async Task<EditUserAccount> GetDetails(string id)
        {
            var userAccount = await _userManager.FindByIdAsync(id);
            return new EditUserAccount()
            {
                Id = userAccount.Id,
                Email = userAccount.Email,
                FullName = userAccount.FullName,
                UserName = userAccount.UserName,
                PhoneNumber = userAccount.PhoneNumber
            };
        }

        public async Task<UserAccountViewModel> GetCurrentUserAccountInfo()
        {
            var userAccount = await _userManager.FindByIdAsync(_authHelper.GetCurrentUserAccountId());
            return new UserAccountViewModel()
            {
                Id = userAccount.Id,
                Email = userAccount.Email,
                UserName = userAccount.UserName,
                FullName = userAccount.FullName,
                PhoneNumber = userAccount.PhoneNumber,
                ProfilePhoto = userAccount.ProfilePhoto,
                EmailConfirmed = userAccount.EmailConfirmed
            };
        }

        public async Task<List<UserAccountViewModel>> Search(UserAccountSearchModel searchModel)
        {
            var query = _userManager.Users.Select(x => new UserAccountViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                ProfilePhoto = x.ProfilePhoto,
                EmailConfirmed = x.EmailConfirmed,
            });

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return await query.OrderBy(x => x.FullName).AsNoTracking().ToListAsync();
        }
    }
}
