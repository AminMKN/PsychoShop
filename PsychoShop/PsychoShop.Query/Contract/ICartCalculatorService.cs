using PsychoShop.Application.Contracts.ShopCart;

namespace PsychoShop.Query.Contract
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
