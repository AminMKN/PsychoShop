using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.ProductSubCategory;

namespace ServiceHost.Areas.Admin.Models
{
    public class ProductSubCategoryAdminCommand
    {
        public string Message { get; set; }
        public SelectList ProductCategories { get; set; }
        public CreateProductSubCategory CreateProductSubCategory { get; set; }
        public EditProductSubCategory EditProductSubCategory { get; set; }
        public ProductSubCategorySearchModel SearchModel { get; set; }
        public List<ProductSubCategoryViewModel> ProductSubCategories { get; set; }
    }
}
