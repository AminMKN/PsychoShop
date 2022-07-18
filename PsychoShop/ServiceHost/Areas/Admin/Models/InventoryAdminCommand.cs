using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.Inventory;

namespace ServiceHost.Areas.Admin.Models
{
    public class InventoryAdminCommand
    {
        public string Message { get; set; }
        public SelectList Products { get; set; }
        public CreateInventory CreateInventory { get; set; }
        public EditInventory EditInventory { get; set; }
        public IncreaseInventory IncreaseInventory { get; set; }
        public ReduceInventory ReduceInventory { get; set; }
        public InventorySearchModel SearchModel { get; set; }
        public List<InventoryViewModel> Inventory { get; set; }
        public List<InventoryOperationViewModel> InventoryOperations { get; set; }
    }
}
