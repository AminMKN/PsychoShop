namespace PsychoShop.Application.Contracts.ShopCart
{
    public interface ICartService
    {
        void Set(Cart cart);
        Cart Get();
    }
}
