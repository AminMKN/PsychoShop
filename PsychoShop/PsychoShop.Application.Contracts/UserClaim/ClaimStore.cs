using System.Security.Claims;

namespace PsychoShop.Application.Contracts.UserClaim
{
    public static class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new(ClaimTypesStore.CommentManagement, true.ToString()),
            new(ClaimTypesStore.DiscountManagement, true.ToString()),
            new(ClaimTypesStore.HomeManagement, true.ToString()),
            new(ClaimTypesStore.InventoryManagement, true.ToString()),
            new(ClaimTypesStore.ProductCategoryManagement, true.ToString()),
            new(ClaimTypesStore.ProductSubCategoryManagement, true.ToString()),
            new(ClaimTypesStore.ProductManagement, true.ToString()),
            new(ClaimTypesStore.ProductPictureManagement, true.ToString()),
            new(ClaimTypesStore.SpecialProductManagement, true.ToString()),
            new(ClaimTypesStore.UserAccountManagement, true.ToString())
        };
    }
}