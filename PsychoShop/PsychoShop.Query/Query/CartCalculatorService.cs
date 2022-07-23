using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.ShopCart;
using PsychoShop.Infrastructure.EFCore;
using PsychoShop.Query.Contract;

namespace PsychoShop.Query.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly PsychoShopContext _context;

        public CartCalculatorService(PsychoShopContext context)
        {
            _context = context;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var discount = _context.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToList();

            foreach (var cartItem in cartItems)
            {
                var productDiscount = discount.FirstOrDefault(x => x.ProductId == cartItem.Id);
                if (productDiscount != null)
                {
                    cartItem.DiscountRate = productDiscount.DiscountRate;
                    cartItem.DiscountAmount = cartItem.TotalAmount * cartItem.DiscountRate / 100;
                    cartItem.PayAmount = cartItem.TotalAmount - cartItem.DiscountAmount;
                    cart.Add(cartItem);
                }
                else
                {
                    cartItem.PayAmount = cartItem.TotalAmount - cartItem.DiscountAmount;
                    cart.Add(cartItem);
                }
            }

            return cart;
        }
    }
}
