using PsychoShop.Application.Contracts.Comment;
using PsychoShop.Query.Contract.Product;
using PsychoShop.Query.Contract.ProductCategory;
using PsychoShop.Query.Contract.ProductSubCategory;

namespace ServiceHost.Models
{
    public class ShopCommand
    {
        public string Message { get; set; }
        public string Search { get; set; }
        public AddComment AddComment { get; set; }
        public ProductQueryModel Product { get; set; }
        public ProductCategoryQueryModel ProductCategory { get; set; }
        public ProductSubCategoryQueryModel ProductSubCategory { get; set; }
        public List<ProductQueryModel> Products { get; set; }
    }
}
