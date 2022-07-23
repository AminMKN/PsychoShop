namespace PsychoShop.Query.Contract.ProductSubCategory
{
    public interface IProductSubCategoryQuery
    {
        Task<ProductSubCategoryQueryModel> GetProductsWithProductSubCategory(string slug);
    }
}
