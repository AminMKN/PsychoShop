using Microsoft.AspNetCore.Identity;

namespace PsychoShop.Application.Contracts.UserClaim
{
    public interface IUserClaimApplication
    {
        Task<IdentityResult> Add(AddOrRemoveClaim command);
        Task<IdentityResult> Remove(AddOrRemoveClaim command);
        Task<AddOrRemoveClaim> GetDetailsForAdd(string id);
        Task<AddOrRemoveClaim> GetDetailsForRemove(string id);
    }
}
