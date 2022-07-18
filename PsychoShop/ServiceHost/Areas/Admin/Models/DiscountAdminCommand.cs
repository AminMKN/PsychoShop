using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.Discount;

namespace ServiceHost.Areas.Admin.Models
{
    public class DiscountAdminCommand
    {
        public string Message { get; set; }
        public SelectList Products { get; set; }
        public DefineDiscount DefineDiscount { get; set; }
        public EditDiscount EditDiscount { get; set; }
        public DiscountSearchModel SearchModel { get; set; }
        public List<DiscountViewModel> Discounts { get; set; }
    }
}