using PsychoShop.Domain.ProductAgg;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.DiscountAgg
{
    public class Discount : EntityBase
    {
        public int DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public Discount(int discountRate, DateTime startDate, DateTime endDate, string reason, int productId)
        {
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            ProductId = productId;
        }

        public void Edit(int discountRate, DateTime startDate, DateTime endDate, string reason, int productId)
        {
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            ProductId = productId;
        }
    }
}
