using PsychoShop.Application.Contracts.ShopCart;

namespace PsychoShop.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        void Cancel(int id);
        double GetAmount(int id);
        int PlaceOrder(Cart cart);
        string PaymentSuccess(int orderId, int refId);
        Task<List<OrderItemViewModel>> GetOrderItemsList(int id);
        Task<List<OrderViewModel>> GetCurrentUserAccountOrders(string userAccountId);
        Task<List<OrderViewModel>> Search(OrderSearchModel searchModel);
    }
}
