using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.Comment;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Query.Contract.Product;
using PsychoShop.Query.Contract.ProductCategory;
using PsychoShop.Query.Contract.ProductSubCategory;
using ServiceHost.Models;

namespace ServiceHost.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductQuery _productQuery;
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IProductSubCategoryQuery _productSubCategoryQuery;
        private readonly ICommentApplication _commentApplication;
        private readonly SignInManager<UserAccount> _signInManager;

        public ShopController(IProductQuery productQuery, IProductCategoryQuery productCategoryQuery, IProductSubCategoryQuery productSubCategoryQuery, ICommentApplication commentApplication, SignInManager<UserAccount> signInManager)
        {
            _productQuery = productQuery;
            _productCategoryQuery = productCategoryQuery;
            _productSubCategoryQuery = productSubCategoryQuery;
            _commentApplication = commentApplication;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string search)
        {
            var command = new ShopCommand()
            {
                Search = search,
                Products = await _productQuery.Search(search)
            };

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> Product(string id)
        {
            var command = new ShopCommand()
            {
                Product = await _productQuery.GetProductDetails(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Product(ShopCommand command)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (ModelState.IsValid)
                {
                    var result = _commentApplication.Add(command.AddComment);
                    command.Message = result.Message;
                }

                return RedirectToAction("Product", "Home");
            }

            return RedirectToAction("SignIn", "UserAccount");
        }


        [HttpGet]
        public async Task<IActionResult> ProductCategory(string id)
        {
            var command = new ShopCommand()
            {
                ProductCategory = await _productCategoryQuery.GetProductsWithProductCategory(id)
            };

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> ProductSubCategory(string id)
        {
            var command = new ShopCommand()
            {
                ProductSubCategory = await _productSubCategoryQuery.GetProductsWithProductSubCategory(id)
            };

            return View(command);
        }
    }
}
