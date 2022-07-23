using Microsoft.AspNetCore.Mvc;
using PsychoShop.Query.Contract.Product;
using ShopManagement.Application.Contracts.Amazing;

namespace ServiceHost.ViewComponents
{
    public class InstantOfferViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public InstantOfferViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productQuery.GetSpecialProductsList(SpecialProductType.InstantOffer);
            return View(products);
        }
    }
}
