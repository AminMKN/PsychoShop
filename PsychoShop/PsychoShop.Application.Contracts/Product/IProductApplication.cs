using PsychoShop.Framework.Application;

namespace PsychoShop.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        OperationResult Remove(int id);
        OperationResult Restore(int id);
        EditProduct GetDetails(int id);
        Task<List<ProductViewModel>> GetProductsList();
        Task<List<ProductViewModel>> Search(ProductSearchModel searchModel);
    }
}
