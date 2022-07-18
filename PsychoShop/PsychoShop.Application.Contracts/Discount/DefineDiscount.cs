using PsychoShop.Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace PsychoShop.Application.Contracts.Discount
{
    public class DefineDiscount
    {
        [Range(1, 99, ErrorMessage = ValidationMessages.ValueNotValid)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Reason { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int ProductId { get; set; }
    }
}
