using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.Discount;
using PsychoShop.Application.Contracts.Product;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class DiscountController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly IDiscountApplication _discountApplication;

        public DiscountController(IProductApplication productApplication, IDiscountApplication discountApplication)
        {
            _productApplication = productApplication;
            _discountApplication = discountApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(DiscountSearchModel searchModel)
        {
            var command = new DiscountAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name"),
                Discounts = await _discountApplication.Search(searchModel)
            };

            return View(command);
        }

        #endregion

        #region Define

        [HttpGet]
        public async Task<IActionResult> Define()
        {
            var command = new DiscountAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name")
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Define(DiscountAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _discountApplication.Define(command.DefineDiscount);
                command.Message = result.Message;
            }

            command.Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name");
            return View(command);
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var command = new DiscountAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name"),
                EditDiscount = _discountApplication.GetDetails(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DiscountAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _discountApplication.Edit(command.EditDiscount);
                command.Message = result.Message;
            }

            command.Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name");
            return View(command);
        }

        #endregion
    }
}
