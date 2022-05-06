using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public ProductCategoryController(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(ProductCategoryModelSet command, ProductCategorySearchModel searchModel)
        {
            command.ProductCategories = await _productCategoryApplication.Search(searchModel);
            return View(command);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductCategoryModelSet());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategoryModelSet command)
        {
            if (ModelState.IsValid)
            {
                var result = _productCategoryApplication.Create(command.CreateProductCategory);
                if (result.IsSuccess)
                {
                    command.IsSuccess = true;
                    command.Message = result.Message;
                    return View(command);
                }
                else
                {
                    command.IsSuccess = false;
                    command.Message = result.Message;
                    return View(command);
                }
            }

            return View(command);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id, ProductCategoryModelSet command)
        {
            command.EditProductCategory = _productCategoryApplication.GetDetails(id);
            return View(command);
        }

        public IActionResult Edit(ProductCategoryModelSet command)
        {
            if (ModelState.IsValid)
            {
                var result = _productCategoryApplication.Edit(command.EditProductCategory);
                if (result.IsSuccess)
                {
                    command.IsSuccess = true;
                    command.Message = result.Message;
                    return View(command);
                }
                else
                {
                    command.IsSuccess = false;
                    command.Message = result.Message;
                    return View(command);
                }
            }

            return View(command);
        }

        #endregion

        #region Remove-Restore

        public IActionResult Remove(int id)
        {
            _productCategoryApplication.Remove(id);
            return RedirectToAction("Index");
        }

        public IActionResult Restore(int id)
        {
            _productCategoryApplication.Restore(id);
            return RedirectToAction("Index");
        }

        #endregion

    }
}
