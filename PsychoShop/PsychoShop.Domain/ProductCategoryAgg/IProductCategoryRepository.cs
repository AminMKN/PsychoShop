using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepositoryBase<int, ProductCategory>
    {
        string GetProductCategorySlug(int id);
        EditProductCategory GetDetails(int id);
        Task<List<ProductCategoryViewModel>> GetProductCategories();
        Task<List<ProductCategoryViewModel>> Search(ProductCategorySearchModel searchModel);
    }
}
