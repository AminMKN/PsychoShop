using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.Inventory;
using PsychoShop.Domain.InventoryAgg;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<int, Inventory>, IInventoryRepository
    {
        private readonly PsychoShopContext _context;
        private readonly IUserAccountRepository _userAccountRepository;

        public InventoryRepository(PsychoShopContext context, IUserAccountRepository userAccountRepository) : base(context)
        {
            _context = context;
            _userAccountRepository = userAccountRepository;
        }

        public Inventory GetInventoryWithProduct(int productId)
        {
            return _context.Inventory
                      .FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetDetails(int id)
        {
            return _context.Inventory.Select(x => new EditInventory()
            {
                Id = x.Id,
                Price = x.Price,
                ProductId = x.ProductId
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<InventoryOperationViewModel>> GetOperationLog(int id)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == id);
            var operations = inventory.InventoryOperations.Select(x => new InventoryOperationViewModel()
            {
                Id = x.Id,
                Count = x.Count,
                Operation = x.Operation,
                OperatorId = x.OperatorId,
                Description = x.Description,
                CurrentCount = x.CurrentCount,
                OperationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();

            foreach (var operation in operations)
            {
                operation.Operator = await _userAccountRepository.GetUserName(operation.OperatorId);
            }

            return operations;
        }

        public async Task<List<InventoryViewModel>> Search(InventorySearchModel searchModel)
        {
            var query = _context.Inventory.Select(x => new InventoryViewModel()
            {
                Id = x.Id,
                Price = x.Price,
                InStock = x.InStock,
                ProductId = x.ProductId,
                Product = x.Product.Name,
                CreationDate = x.CreationDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount()
            });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.InStock)
                query = query.Where(x => !x.InStock);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}