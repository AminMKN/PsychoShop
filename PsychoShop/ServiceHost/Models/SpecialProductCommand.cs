using PsychoShop.Query.Contract.Product;

namespace ServiceHost.Models
{
    public class SpecialProductCommand
    {
        public List<ProductQueryModel> Products { get; set; }
    }
}
