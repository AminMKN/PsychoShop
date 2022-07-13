using PsychoShop.Domain.ProductAgg;
using PsychoShop.Domain.ProductCategoryAgg;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.ProductSubCategoryAgg
{
    public class ProductSubCategory : EntityBase
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public int ProductCategoryId { get; set; }
        public bool IsRemoved { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public List<Product> Products { get; private set; }

        public ProductSubCategory(string name, string slug, string keywords, string metaDescription, int productCategoryId)
        {
            Name = name;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            ProductCategoryId = productCategoryId;
            IsRemoved = false;
            Products = new List<Product>();
        }

        public void Edit(string name, string slug, string keywords, string metaDescription, int productCategoryId)
        {
            Name = name;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            ProductCategoryId = productCategoryId;
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
