using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Admin.Models
{
    public class ProductPictureAdminCommand
    {
        public string Message { get; set; }
        public SelectList Products { get; set; }
        public CreateProductPicture CreateProductPicture { get; set; }
        public EditProductPicture EditProductPicture { get; set; }
        public ProductPictureSearchModel SearchModel { get; set; }
        public List<ProductPictureViewModel> ProductPictures { get; set; }
    }
}
