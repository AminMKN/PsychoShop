using PsychoShop.Application.Contracts.UserClaim;

namespace PsychoShop.Domain.UserClaim
{
    public interface IUserClaimRepository
    {
        Task<AddOrRemoveClaim> GetDetailsForAdd(string id);
        Task<AddOrRemoveClaim> GetDetailsForRemove(string id);
    }
}
