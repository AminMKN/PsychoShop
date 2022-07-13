using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.Product;

namespace ServiceHost.Areas.Admin.Models
{
    public class ProductAdminCommand
    {
        public string Message { get; set; }
        public SelectList ProductCategories{ get; set; }
        public CreateProduct CreateProduct { get; set; }
        public EditProduct EditProduct { get; set; }
        public ProductSearchModel SearchModel { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}