using Microsoft.AspNetCore.Mvc;
using PsychoShop.Query.Contract.Product;

namespace ServiceHost.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public ProductViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productQuery.GetProductsList();
            return View(products);
        }
    }
}
