using PsychoShop.Application.Contracts.Product;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.ProductAgg
{
    public interface IProductRepository : IRepositoryBase<int, Product>
    {
        EditProduct GetDetails(int id);
        Task<List<ProductViewModel>> GetProductsList();
        Task<List<ProductViewModel>> Search(ProductSearchModel searchModel);
    }
}
