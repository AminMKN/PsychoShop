using Microsoft.AspNetCore.Identity;
using PsychoShop.Application.Contracts.UserClaim;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Domain.UserClaim;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class UserClaimRepository : IUserClaimRepository
    {
        private readonly UserManager<UserAccount> _userManager;

        public UserClaimRepository(UserManager<UserAccount> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddOrRemoveClaim> GetDetailsForAdd(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var claims = ClaimStore.AllClaims;
            var userClaims = await _userManager.GetClaimsAsync(user);
            var validClaims = claims.Where(x => userClaims.All(c => c.Type != x.Type))
                .Select(x => new UserClaimDetails()
                {
                    ClaimName = x.Type
                }).ToList();

            return new AddOrRemoveClaim()
            {
                UserAccountId = id,
                UserClaimDetails = validClaims
            };
        }

        public async Task<AddOrRemoveClaim> GetDetailsForRemove(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var validClaims = userClaims
                .Select(x => new UserClaimDetails()
                {
                    ClaimName = x.Type
                }).ToList();

            return new AddOrRemoveClaim()
            {
                UserAccountId = id,
                UserClaimDetails = validClaims
            };
        }
    }
}
