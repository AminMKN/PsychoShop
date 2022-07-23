using Microsoft.AspNetCore.Mvc;
using PsychoShop.Query.Contract.Product;
using ShopManagement.Application.Contracts.Amazing;

namespace ServiceHost.ViewComponents
{
    public class AmazingSuperMarketViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public AmazingSuperMarketViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productQuery.GetSpecialProductsList(SpecialProductType.AmazingSuperMarket);
            return View(products);
        }
    }
}
