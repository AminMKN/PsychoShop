using PsychoShop.Domain.ProductAgg;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.OrderAgg
{
    public class OrderItem : EntityBase
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
        public int DiscountRate { get; set; }
        public double Price { get; set; }
        public Order Order { get; set; }

        public OrderItem(int productId, int count, int discountRate, double price)
        {
            ProductId = productId;
            Count = count;
            DiscountRate = discountRate;
            Price = price;
        }
    }
}