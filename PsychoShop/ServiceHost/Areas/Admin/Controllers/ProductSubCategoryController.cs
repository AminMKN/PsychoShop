using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Application.Contracts.ProductSubCategory;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class ProductSubCategoryController : Controller
    {
        private readonly IProductCategoryApplication _productCategoryApplication;
        private readonly IProductSubCategoryApplication _productSubCategoryApplication;

        public ProductSubCategoryController(IProductSubCategoryApplication productSubCategoryApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productSubCategoryApplication = productSubCategoryApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(ProductSubCategorySearchModel searchModel)
        {
            var command = new ProductSubCategoryAdminCommand()
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesList(), "Id", "Name"),
                ProductSubCategories = await _productSubCategoryApplication.Search(searchModel)
            };

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> ProductSubCategoriesJson(int id)
        {
            var productSubCategoriesJson = await _productSubCategoryApplication.GetProductSubCategoriesJson(id);
            return new JsonResult(productSubCategoriesJson);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var command = new ProductSubCategoryAdminCommand()
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesList(), "Id", "Name")
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductSubCategoryAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _productSubCategoryApplication.Create(command.CreateProductSubCategory);
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
            var command = new ProductSubCategoryAdminCommand()
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesList(), "Id", "Name"),
                EditProductSubCategory = _productSubCategoryApplication.GetDetails(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductSubCategoryAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _productSubCategoryApplication.Edit(command.EditProductSubCategory);
                command.Message = result.Message;
            }

            command.ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesList(), "Id", "Name");
            return View(command);
        }

        #endregion

        #region Remove-Restore

        public IActionResult Remove(int id)
        {
            _productSubCategoryApplication.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Restore(int id)
        {
            _productSubCategoryApplication.Restore(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
