using Microsoft.AspNetCore.Mvc;
using PsychoShop.Query.Contract.Product;
using ShopManagement.Application.Contracts.Amazing;

namespace ServiceHost.ViewComponents
{
    public class AmazingOfferViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public AmazingOfferViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productQuery.GetSpecialProductsList(SpecialProductType.AmazingOffer);
            return View(products);
        }
    }
}
