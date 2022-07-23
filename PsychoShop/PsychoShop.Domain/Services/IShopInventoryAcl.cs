using PsychoShop.Domain.OrderAgg;

namespace PsychoShop.Domain.Services
{
    public interface IShopInventoryAcl
    {
        bool ReduceFromInventory(List<OrderItem> orderItems);
    }
}
