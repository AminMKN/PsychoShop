using PsychoShop.Query.Contract.Comment;
using PsychoShop.Query.Contract.ProductPicture;

namespace PsychoShop.Query.Contract.Product
{
    public class ProductQueryModel
    {
        public int Id { get; set; }
        public int DiscountRate { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public string Property { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public double Price { get; set; }
        public double PriceWithDiscount { get; set; }
        public bool HasDiscount { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
        public List<ProductPictureQueryModel> Pictures { get; set; }
    }
}