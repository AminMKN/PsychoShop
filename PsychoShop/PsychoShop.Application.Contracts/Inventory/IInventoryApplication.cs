using PsychoShop.Framework.Application;

namespace PsychoShop.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(ReduceInventory command);
        OperationResult ReduceAfterPurchase(List<ReduceInventoryAfterPurchase> command);
        EditInventory GetDetails(int id);
        Task<List<InventoryOperationViewModel>> GetOperationLog(int id);
        Task<List<InventoryViewModel>> Search(InventorySearchModel searchModel);
    }
}
