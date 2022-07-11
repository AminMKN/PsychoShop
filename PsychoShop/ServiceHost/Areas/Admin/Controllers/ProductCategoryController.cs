using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize(Policy = "ProductCategoryPolicy")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public ProductCategoryController(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(ProductCategorySearchModel searchModel)
        {
            var command = new ProductCategoryAdminModel()
            {
                ProductCategories = await _productCategoryApplication.Search(searchModel)
            };

            return View(command);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductCategoryAdminModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategoryAdminModel command)
        {
            if (ModelState.IsValid)
            {
                var result = _productCategoryApplication.Create(command.CreateProductCategory);
                command.Message = result.Message;
            }

            return View(command);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var command = new ProductCategoryAdminModel()
            {
                EditProductCategory = _productCategoryApplication.GetDetails(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCategoryAdminModel command)
        {
            if (ModelState.IsValid)
            {
                var result = _productCategoryApplication.Edit(command.EditProductCategory);
                command.Message = result.Message;
            }

            return View(command);
        }

        #endregion

        #region Remove-Restore

        public IActionResult Remove(int id)
        {
            _productCategoryApplication.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Restore(int id)
        {
            _productCategoryApplication.Restore(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
