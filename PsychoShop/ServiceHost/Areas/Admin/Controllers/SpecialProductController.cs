using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.Product;
using PsychoShop.Application.Contracts.SpecialProduct;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class SpecialProductController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly ISpecialProductApplication _specialProductApplication;

        public SpecialProductController(IProductApplication productApplication, ISpecialProductApplication specialProductApplication)
        {
            _productApplication = productApplication;
            _specialProductApplication = specialProductApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(SpecialProductSearchModel searchModel)
        {
            var command = new SpecialProductAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name"),
                SpecialProducts = await _specialProductApplication.Search(searchModel)
            };

            return View(command);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var command = new SpecialProductAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name")
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialProductAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _specialProductApplication.Create(command.CreateSpecialProduct);
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
            var command = new SpecialProductAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name"),
                EditSpecialProduct = _specialProductApplication.GetDetails(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialProductAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _specialProductApplication.Edit(command.EditSpecialProduct);
                command.Message = result.Message;
            }

            command.Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name");
            return View(command);
        }

        #endregion
    }
}
