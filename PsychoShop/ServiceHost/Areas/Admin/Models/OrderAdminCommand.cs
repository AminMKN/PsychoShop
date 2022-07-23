using PsychoShop.Application.Contracts.Order;

namespace ServiceHost.Areas.Admin.Models
{
    public class OrderAdminCommand
    {
        public OrderSearchModel SearchModel { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
