using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.InventoryAgg
{
    public class InventoryOperation : EntityBase
    {
        public int Count { get; set; }
        public int CurrentCount { get; set; }
        public string OperatorId { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public int InventoryId { get; set; }
        public bool Operation { get; set; }
        public Inventory Inventory { get; set; }

        public InventoryOperation(int count, int currentCount, string operatorId, string description, int orderId,
            int inventoryId, bool operation)
        {
            Count = count;
            CurrentCount = currentCount;
            OperatorId = operatorId;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
            Operation = operation;
        }
    }
}
