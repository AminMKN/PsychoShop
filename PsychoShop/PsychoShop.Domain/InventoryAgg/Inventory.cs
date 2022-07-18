using PsychoShop.Domain.ProductAgg;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public double Price { get; set; }
        public int ProductId { get; set; }
        public bool InStock { get; set; }
        public Product Product { get; set; }
        public List<InventoryOperation> InventoryOperations { get; set; }

        public Inventory(double price, int productId)
        {
            Price = price;
            ProductId = productId;
            InStock = false;
            InventoryOperations = new List<InventoryOperation>();
        }

        public void Edit(double price, int productId)
        {
            Price = price;
            ProductId = productId;
        }

        public int CalculateCurrentCount()
        {
            var plus = InventoryOperations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = InventoryOperations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(int count, string operatorId, string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(count, currentCount, operatorId, description, 0, Id, true);
            InventoryOperations.Add(operation);
            InStock = currentCount > 0;
        }

        public void Reduce(int count, string operatorId, string description)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(count, currentCount, operatorId, description, 0, Id, false);
            InventoryOperations.Add(operation);
            InStock = currentCount > 0;
        }

        public void ReduceAfterPurchase(int count, string operatorId, string description, int orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(count, currentCount, operatorId, description, orderId, Id, false);
            InventoryOperations.Add(operation);
            InStock = currentCount > 0;
        }
    }
}
