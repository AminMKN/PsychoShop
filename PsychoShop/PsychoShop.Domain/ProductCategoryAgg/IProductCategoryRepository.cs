using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepositoryBase<int, ProductCategory>
    {
        EditProductCategory GetDetails(int id);
        Task<List<ProductCategoryViewModel>> GetProductCategoriesList();
        Task<List<ProductCategoryViewModel>> Search(ProductCategorySearchModel searchModel);
    }
}
