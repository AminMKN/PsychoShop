using PsychoShop.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Admin.Models
{
    public class ProductCategoryModelSet
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ProductCategorySearchModel SearchModel { get; set; }
        public EditProductCategory EditProductCategory { get; set; }
        public CreateProductCategory CreateProductCategory { get; set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
    }
}
