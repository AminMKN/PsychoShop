using PsychoShop.Query.Contract.Product;
using PsychoShop.Query.Contract.ProductSubCategory;

namespace PsychoShop.Query.Contract.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public List<ProductQueryModel> Products { get; set; }
        public List<ProductSubCategoryQueryModel> ProductSubCategories { get; set; }
    }
}