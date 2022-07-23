using PsychoShop.Application.Contracts.Order;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.OrderAgg
{
    public interface IOrderRepository : IRepositoryBase<int, Order>
    {
        double GetAmount(int id);
        Task<List<OrderItemViewModel>> GetOrderItemsList(int id);
        Task<List<OrderViewModel>> GetCurrentUserAccountOrders(string userAccountId);
        Task<List<OrderViewModel>> Search(OrderSearchModel searchModel);
    }
}