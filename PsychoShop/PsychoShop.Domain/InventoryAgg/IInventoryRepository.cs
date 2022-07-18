using PsychoShop.Application.Contracts.Inventory;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepositoryBase<int, Inventory>
    {
        Inventory GetInventoryWithProduct(int productId);
        EditInventory GetDetails(int id);
        Task<List<InventoryOperationViewModel>> GetOperationLog(int id);
        Task<List<InventoryViewModel>> Search(InventorySearchModel searchModel);
    }
}
