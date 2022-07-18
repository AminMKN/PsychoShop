using PsychoShop.Application.Contracts.Inventory;
using PsychoShop.Domain.InventoryAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Application.AuthHelper;

namespace PsychoShop.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository, IAuthHelper authHelper)
        {
            _authHelper = authHelper;
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var inventory = new Inventory(command.Price, command.ProductId);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            inventory.Edit(command.Price, command.ProductId);
            _inventoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            var operatorId = _authHelper.GetCurrentUserAccountId();
            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (inventory.CalculateCurrentCount() < command.Count)
                return operation.Failed(ApplicationMessages.TheCountIsMoreTheInventory);

            var operatorId = _authHelper.GetCurrentUserAccountId();
            inventory.Reduce(command.Count, operatorId, command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult ReduceAfterPurchase(List<ReduceInventoryAfterPurchase> command)
        {
            var operation = new OperationResult();
            var operatorId = _authHelper.GetCurrentUserAccountId();
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetInventoryWithProduct(item.ProductId);
                if (inventory != null)
                    inventory.ReduceAfterPurchase(item.Count, operatorId, item.Description, item.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public EditInventory GetDetails(int id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public async Task<List<InventoryOperationViewModel>> GetOperationLog(int id)
        {
            return await _inventoryRepository.GetOperationLog(id);
        }

        public async Task<List<InventoryViewModel>> Search(InventorySearchModel searchModel)
        {
            return await _inventoryRepository.Search(searchModel);
        }
    }
}