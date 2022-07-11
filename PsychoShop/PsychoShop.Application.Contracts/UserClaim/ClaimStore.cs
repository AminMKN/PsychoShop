using System.Security.Claims;

namespace PsychoShop.Application.Contracts.UserClaim
{
    public static class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new(ClaimTypesStore.HomeManagement, true.ToString()),
            new(ClaimTypesStore.ProductCategoryManagement, true.ToString()),
            new(ClaimTypesStore.UserAccountManagement, true.ToString())
        };
    }
}