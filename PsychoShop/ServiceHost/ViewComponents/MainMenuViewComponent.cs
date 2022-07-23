using Microsoft.AspNetCore.Mvc;
using PsychoShop.Query.Contract.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public MainMenuViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var productCategories = await _productCategoryQuery.GetProductCategoriesList();
            return View(productCategories);
        }
    }
}
