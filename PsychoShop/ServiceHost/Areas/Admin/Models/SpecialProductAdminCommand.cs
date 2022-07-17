using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.SpecialProduct;

namespace ServiceHost.Areas.Admin.Models
{
    public class SpecialProductAdminCommand
    {
        public string Message { get; set; }
        public SelectList Products { get; set; }
        public CreateSpecialProduct CreateSpecialProduct { get; set; }
        public EditSpecialProduct EditSpecialProduct { get; set; }
        public SpecialProductSearchModel SearchModel { get; set; }
        public List<SpecialProductViewModel> SpecialProducts { get; set; }
    }
}