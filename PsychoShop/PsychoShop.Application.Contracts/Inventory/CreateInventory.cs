using PsychoShop.Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace PsychoShop.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public double Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int ProductId { get; set; }
    }
}