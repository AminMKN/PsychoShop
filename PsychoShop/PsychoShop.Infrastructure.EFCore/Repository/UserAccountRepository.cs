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

        public async Task<EditUserAccount> GetDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return new EditUserAccount()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<UserAccountViewModel> GetCurrentUserAccountInfo()
        {
            var user = await _userManager.FindByIdAsync(_authHelper.GetCurrentUserAccountId());
            return new UserAccountViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                ProfilePhoto = user.ProfilePhoto,
                EmailConfirmed = user.EmailConfirmed
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
