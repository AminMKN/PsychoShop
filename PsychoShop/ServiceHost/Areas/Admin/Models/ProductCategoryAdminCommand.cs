using PsychoShop.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Admin.Models
{
    public class ProductCategoryAdminCommand
    {
        public string Message { get; set; }
        public CreateProductCategory CreateProductCategory { get; set; }
        public EditProductCategory EditProductCategory { get; set; }
        public ProductCategorySearchModel SearchModel { get; set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
    }
}
