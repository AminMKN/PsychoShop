using PsychoShop.Application.Contracts.ProductSubCategory;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.ProductSubCategoryAgg
{
    public interface IProductSubCategoryRepository : IRepositoryBase<int, ProductSubCategory>
    {
        EditProductSubCategory GetDetails(int id);
        Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesList();
        Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesJson(int id);
        Task<List<ProductSubCategoryViewModel>> Search(ProductSubCategorySearchModel searchModel);
    }
}
