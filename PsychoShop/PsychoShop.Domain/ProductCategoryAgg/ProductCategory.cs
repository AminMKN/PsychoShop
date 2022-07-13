using PsychoShop.Domain.ProductAgg;
using PsychoShop.Domain.ProductSubCategoryAgg;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IsRemoved { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductSubCategory> ProductSubCategories { get; set; }

        public ProductCategory(string name, string slug, string keywords, string metaDescription)
        {
            Name = name;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            IsRemoved = false;
            Products = new List<Product>();
            ProductSubCategories = new List<ProductSubCategory>();
        }

        public void Edit(string name, string slug, string keywords, string metaDescription)
        {
            Name = name;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
