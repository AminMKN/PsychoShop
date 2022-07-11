using Microsoft.AspNetCore.Identity;
using PsychoShop.Application.Contracts.UserClaim;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Domain.UserClaim;
using System.Security.Claims;

namespace PsychoShop.Application
{
    public class UserClaimApplication : IUserClaimApplication
    {
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly UserManager<UserAccount> _userManager;

        public UserClaimApplication(UserManager<UserAccount> userManager, IUserClaimRepository userClaimRepository)
        {
            _userManager = userManager;
            _userClaimRepository = userClaimRepository;
        }

        public async Task<IdentityResult> Add(AddOrRemoveClaim command)
        {
            var user = await _userManager.FindByIdAsync(command.UserAccountId);
            var requestClaims = command.UserClaimDetails
                .Where(x => x.IsSelected)
                .Select(x => new Claim(x.ClaimName, true.ToString())).ToList();
            return await _userManager.AddClaimsAsync(user, requestClaims);
        }

        public async Task<IdentityResult> Remove(AddOrRemoveClaim command)
        {
            var user = await _userManager.FindByIdAsync(command.UserAccountId);
            var requestClaims = command.UserClaimDetails
                .Where(x => x.IsSelected)
                .Select(x => new Claim(x.ClaimName, true.ToString())).ToList();
            return await _userManager.RemoveClaimsAsync(user, requestClaims);
        }

        public async Task<AddOrRemoveClaim> GetDetailsForAdd(string id)
        {
            return await _userClaimRepository.GetDetailsForAdd(id);
        }

        public async Task<AddOrRemoveClaim> GetDetailsForRemove(string id)
        {
            return await _userClaimRepository.GetDetailsForRemove(id);
        }
    }
}
