using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.Order;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class OrderController : Controller
    {
        private readonly IOrderApplication _orderApplication;

        public OrderController(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        #region Index

        public async Task<IActionResult> Index(OrderSearchModel searchModel)
        {
            var command = new OrderAdminCommand()
            {
                Orders = await _orderApplication.Search(searchModel)
            };

            return View(command);
        }

        public async Task<IActionResult> Items(int id)
        {
            var command = new OrderAdminCommand()
            {
                OrderItems = await _orderApplication.GetOrderItemsList(id)
            };

            return View(command);
        }

        #endregion

        #region Confirm-Cancel

        public IActionResult Confirm(int id)
        {
            _orderApplication.PaymentSuccess(id, 0);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cancel(int id)
        {
            _orderApplication.Cancel(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
