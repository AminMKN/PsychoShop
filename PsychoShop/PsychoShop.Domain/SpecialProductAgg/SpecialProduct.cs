using PsychoShop.Domain.ProductAgg;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.SpecialProductAgg
{
    public class SpecialProduct : EntityBase
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Type { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public SpecialProduct(DateTime startDate, DateTime endDate, int type, int productId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
            ProductId = productId;
        }

        public void Edit(DateTime startDate, DateTime endDate, int type, int productId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
            ProductId = productId;
        }
    }
}
