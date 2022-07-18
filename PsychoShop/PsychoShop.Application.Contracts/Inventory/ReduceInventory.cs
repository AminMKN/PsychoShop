using PsychoShop.Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace PsychoShop.Application.Contracts.Inventory
{
    public class ReduceInventory
    {
        public int InventoryId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int Count { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }
    }
}