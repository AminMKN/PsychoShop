using PsychoShop.Application.Contracts.Inventory;
using PsychoShop.Domain.OrderAgg;
using PsychoShop.Domain.Services;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> orderItems)
        {
            var command = new List<ReduceInventoryAfterPurchase>();
            foreach (var orderItem in orderItems)
            {
                var item = new ReduceInventoryAfterPurchase(orderItem.ProductId, orderItem.OrderId, orderItem.Count, "خرید مشتری");
                command.Add(item);
            }

            return _inventoryApplication.ReduceAfterPurchase(command).IsSuccess;
        }
    }
}
