using PsychoShop.Application.Contracts.ShopCart;

namespace PsychoShop.Query.Contract.Product
{
    public interface IProductQuery
    {
        Task<List<ProductQueryModel>> GetProductsList();
        Task<List<ProductQueryModel>> GetProductsHaveDiscount();
        Task<List<ProductQueryModel>> GetSpecialProductsList(int type);
        Task<List<ProductQueryModel>> Search(string search);
        Task<ProductQueryModel> GetProductDetails(string slug);
        Task<List<CartItem>> CheckInventoryStatus(List<CartItem> cartItems);
    }
}
