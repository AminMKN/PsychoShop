using PsychoShop.Application.Contracts.Order;
using PsychoShop.Application.Contracts.ShopCart;
using PsychoShop.Domain.OrderAgg;
using PsychoShop.Domain.Services;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Application.AuthHelper;

namespace PsychoShop.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopInventoryAcl _shopInventoryAcl;

        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IShopInventoryAcl shopInventoryAcl)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _shopInventoryAcl = shopInventoryAcl;
        }

        public void Cancel(int id)
        {
            var order = _orderRepository.Get(id);
            order.Cancel();
            _orderRepository.SaveChanges();
        }

        public double GetAmount(int id)
        {
            return _orderRepository.GetAmount(id);
        }

        public int PlaceOrder(Cart cart)
        {
            var userAccountId = _authHelper.GetCurrentUserAccountId();
            var order = new Order(userAccountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount,
                cart.PostalCode, cart.PhoneNumber, cart.Address);

            foreach (var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.DiscountRate, cartItem.Price);
                order.Add(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public string PaymentSuccess(int orderId, int refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSuccess(refId);
            var issueTrackingNo = CodeGenerator.Generate("S");
            order.SetIssueTrackingNo(issueTrackingNo);
            if (_shopInventoryAcl.ReduceFromInventory(order.OrderItems))
            {
                _orderRepository.SaveChanges();
                return issueTrackingNo;
            }

            return null;
        }

        public async Task<List<OrderItemViewModel>> GetOrderItemsList(int id)
        {
            return await _orderRepository.GetOrderItemsList(id);
        }

        public async Task<List<OrderViewModel>> GetCurrentUserAccountOrders(string userAccountId)
        {
            return await _orderRepository.GetCurrentUserAccountOrders(userAccountId);
        }

        public async Task<List<OrderViewModel>> Search(OrderSearchModel searchModel)
        {
            return await _orderRepository.Search(searchModel);
        }
    }
}
