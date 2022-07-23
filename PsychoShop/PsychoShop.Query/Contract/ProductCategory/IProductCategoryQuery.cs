namespace PsychoShop.Query.Contract.ProductCategory
{
    public interface IProductCategoryQuery
    {
        Task<List<ProductCategoryQueryModel>> GetProductCategoriesList();
        Task<ProductCategoryQueryModel> GetProductsWithProductCategory(string slug);
    }
}