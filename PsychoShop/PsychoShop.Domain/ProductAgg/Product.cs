using PsychoShop.Domain.ProductCategoryAgg;
using PsychoShop.Domain.ProductSubCategoryAgg;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public string Information { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public bool IsRemoved { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductSubCategory ProductSubCategory { get; set; }

        public Product(string name, string slug, string code, string information, string property, string description,
            string picture, string pictureAlt, string pictureTitle, string keywords, string metaDescription,
            int productCategoryId, int productSubCategoryId)
        {
            Name = name;
            Slug = slug;
            Code = code;
            Information = information;
            Property = property;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            ProductCategoryId = productCategoryId;
            ProductSubCategoryId = productSubCategoryId;
            IsRemoved = false;
        }

        public void Edit(string name, string slug, string code, string information, string property, string description,
            string picture, string pictureAlt, string pictureTitle, string keywords, string metaDescription,
            int productCategoryId, int productSubCategoryId)
        {
            Name = name;
            Slug = slug;
            Code = code;
            Information = information;
            Property = property;
            Description = description;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            ProductCategoryId = productCategoryId;
            ProductSubCategoryId = productSubCategoryId;
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
