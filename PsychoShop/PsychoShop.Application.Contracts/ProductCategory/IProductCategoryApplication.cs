using PsychoShop.Framework.Application;

namespace PsychoShop.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        OperationResult Remove(int id);
        OperationResult Restore(int id);
        EditProductCategory GetDetails(int id);
        Task<List<ProductCategoryViewModel>> GetProductCategoriesList();
        Task<List<ProductCategoryViewModel>> Search(ProductCategorySearchModel searchModel);
    }
}
