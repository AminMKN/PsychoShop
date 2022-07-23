using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.Product;
using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Application.Contracts.ProductSubCategory;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize(Policy = "ProductPolicy")]
    public class ProductController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        private readonly IProductSubCategoryApplication _productSubCategoryApplication;

        public ProductController(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication, IProductSubCategoryApplication productSubCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
            _productSubCategoryApplication = productSubCategoryApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(ProductSearchModel searchModel)
        {
            var command = new ProductAdminCommand()
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesList(), "Id", "Name"),
                Products = await _productApplication.Search(searchModel)
            };

            return View(command);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var command = new ProductAdminCommand()
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesList(), "Id", "Name"),
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _productApplication.Create(command.CreateProduct);
                command.Message = result.Message;
            }

            command.ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesList(), "Id", "Name");
            return View(command);
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var command = new ProductAdminCommand()
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesList(), "Id", "Name"),
                EditProduct = _productApplication.GetDetails(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _productApplication.Edit(command.EditProduct);
                command.Message = result.Message;
            }

            command.ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesList(), "Id", "Name");
            return View(command);
        }

        #endregion

        #region Remove-Restore

        public IActionResult Remove(int id)
        {
            _productApplication.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Restore(int id)
        {
            _productApplication.Restore(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
