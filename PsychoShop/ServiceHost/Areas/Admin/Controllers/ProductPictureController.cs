using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.Product;
using PsychoShop.Application.Contracts.ProductPicture;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize(Policy = "ProductPicturePolicy")]
    public class ProductPictureController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;

        public ProductPictureController(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(ProductPictureSearchModel searchModel)
        {
            var command = new ProductPictureAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name"),
                ProductPictures = await _productPictureApplication.Search(searchModel)
            };

            return View(command);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var command = new ProductPictureAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name")
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductPictureAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _productPictureApplication.Create(command.CreateProductPicture);
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
            var command = new ProductPictureAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name"),
                EditProductPicture = _productPictureApplication.GetDetails(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductPictureAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _productPictureApplication.Edit(command.EditProductPicture);
                command.Message = result.Message;
            }

            command.Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name");
            return View(command);
        }

        #endregion

        #region Remove-Restore

        public IActionResult Remove(int id)
        {
            _productPictureApplication.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Restore(int id)
        {
            _productPictureApplication.Restore(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
