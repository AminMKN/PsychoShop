using PsychoShop.Query.Contract.Product;

namespace PsychoShop.Query.Contract.ProductSubCategory
{
    public class ProductSubCategoryQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public List<ProductQueryModel> Products { get; set; }
    }
}
