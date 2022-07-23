using PsychoShop.Application.Contracts.ShopCart;
using PsychoShop.Framework.Application.ZarinPal;

namespace ServiceHost.Models
{
    public class PaymentCommand
    {
        public string CookieName { get; set; } = "PsychoShop-Cart-Items";
        public string EmptyCart { get; set; }
        public PaymentResult PaymentResult { get; set; }
        public Cart Cart { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
