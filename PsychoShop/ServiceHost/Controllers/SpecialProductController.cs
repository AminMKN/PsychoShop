using Microsoft.AspNetCore.Mvc;
using PsychoShop.Query.Contract.Product;
using ServiceHost.Models;
using ShopManagement.Application.Contracts.Amazing;

namespace ServiceHost.Controllers
{
    public class SpecialProductController : Controller
    {
        private readonly IProductQuery _productQuery;

        public SpecialProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        [HttpGet]
        public async Task<IActionResult> Discount()
        {
            var command = new SpecialProductCommand()
            {
                Products = await _productQuery.GetProductsHaveDiscount()
            };

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> AmazingOffer()
        {
            var command = new SpecialProductCommand()
            {
                Products = await _productQuery.GetSpecialProductsList(SpecialProductType.AmazingOffer)
            };

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> AmazingSuperMarket()
        {
            var command = new SpecialProductCommand()
            {
                Products = await _productQuery.GetSpecialProductsList(SpecialProductType.AmazingSuperMarket)
            };

            return View(command);
        }
    }
}
