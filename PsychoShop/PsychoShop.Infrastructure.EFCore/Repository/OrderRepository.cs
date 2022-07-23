using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.Order;
using PsychoShop.Domain.OrderAgg;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<int, Order>, IOrderRepository
    {
        private readonly PsychoShopContext _context;
        private readonly IUserAccountRepository _userAccountRepository;

        public OrderRepository(PsychoShopContext context, IUserAccountRepository userAccountRepository) : base(context)
        {
            _context = context;
            _userAccountRepository = userAccountRepository;
        }

        public double GetAmount(int id)
        {
            var order = _context.Orders.Select(x => new { x.Id, x.PayAmount }).FirstOrDefault(x => x.Id == id);
            if (order != null)
                return order.PayAmount;

            return 0;
        }

        public async Task<List<OrderItemViewModel>> GetOrderItemsList(int id)
        {
            var products = await _context.Products.Select(x => new { x.Id, x.Name }).AsNoTracking().ToListAsync();
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.OrderItems.Select(x => new OrderItemViewModel()
            {
                Id = x.Id,
                Count = x.Count,
                Price = x.Price,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate
            }).OrderByDescending(x => x.Id).ToList();

            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }

            return items;
        }

        public async Task<List<OrderViewModel>> GetCurrentUserAccountOrders(string userAccountId)
        {
            return await _context.Orders.Where(x => x.UserAccountId == userAccountId).Select(x => new OrderViewModel()
            {
                Id = x.Id,
                RefId = x.RefId,
                IsPaid = x.IsPaid,
                UserAccountId = x.UserAccountId,
                Address = x.Address,
                PayAmount = x.PayAmount,
                IsCanceled = x.IsCanceled,
                PostalCode = x.PostalCode,
                TotalAmount = x.TotalAmount,
                PhoneNumber = x.PhoneNumber,
                DiscountAmount = x.DiscountAmount,
                IssueTrackingNo = x.IssueTrackingNo,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<OrderViewModel>> Search(OrderSearchModel searchModel)
        {
            var query = _context.Orders.Select(x => new OrderViewModel()
            {
                Id = x.Id,
                RefId = x.RefId,
                IsPaid = x.IsPaid,
                UserAccountId = x.UserAccountId,
                Address = x.Address,
                PayAmount = x.PayAmount,
                IsCanceled = x.IsCanceled,
                PostalCode = x.PostalCode,
                TotalAmount = x.TotalAmount,
                PhoneNumber = x.PhoneNumber,
                DiscountAmount = x.DiscountAmount,
                IssueTrackingNo = x.IssueTrackingNo,
                CreationDate = x.CreationDate.ToFarsi(),
            });

            query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

            if (!string.IsNullOrWhiteSpace(searchModel.IssueTrackingNo))
                query = query.Where(x => x.IssueTrackingNo.Contains(searchModel.IssueTrackingNo));

            var orders = query.OrderByDescending(x => x.Id).AsNoTracking().ToList();

            foreach (var order in orders)
            {
                order.FullName = await _userAccountRepository.GetUserName(order.UserAccountId);
            }

            return orders;
        }
    }
}
