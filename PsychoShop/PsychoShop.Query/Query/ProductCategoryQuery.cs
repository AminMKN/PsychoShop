using Microsoft.EntityFrameworkCore;
using PsychoShop.Domain.ProductAgg;
using PsychoShop.Domain.ProductSubCategoryAgg;
using PsychoShop.Infrastructure.EFCore;
using PsychoShop.Query.Contract.Product;
using PsychoShop.Query.Contract.ProductCategory;
using PsychoShop.Query.Contract.ProductSubCategory;

namespace PsychoShop.Query.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly PsychoShopContext _context;

        public ProductCategoryQuery(PsychoShopContext context)
        {
            _context = context;
        }

        public async Task<List<ProductCategoryQueryModel>> GetProductCategoriesList()
        {
            return await _context.ProductCategories
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ProductSubCategories = MapProductSubCategories(x.ProductSubCategories)
                }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<ProductCategoryQueryModel> GetProductsWithProductCategory(string slug)
        {
            var inventory = await _context.Inventory
             .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _context.Discounts
                   .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                   .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var category = await _context.ProductCategories
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Products = MapProducts(x.Products)
                }).OrderByDescending(x => x.Id).AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug);

            foreach (var product in category.Products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    QueryHelper.CalculatePrice(productInventory.Price, product);
                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        QueryHelper.CalculateDiscount(productDiscount.DiscountRate, productInventory.Price, product);
                    }
                }
            }

            return category;
        }

        private static List<ProductQueryModel> MapProducts(IEnumerable<Product> products)
        {
            return products
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).OrderByDescending(x => x.Id).ToList();
        }

        private static List<ProductSubCategoryQueryModel> MapProductSubCategories(IEnumerable<ProductSubCategory> productSubCategories)
        {
            return productSubCategories
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductSubCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug
                }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
