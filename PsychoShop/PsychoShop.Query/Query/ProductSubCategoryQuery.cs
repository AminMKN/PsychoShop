using Microsoft.EntityFrameworkCore;
using PsychoShop.Domain.ProductAgg;
using PsychoShop.Infrastructure.EFCore;
using PsychoShop.Query.Contract.Product;
using PsychoShop.Query.Contract.ProductSubCategory;

namespace PsychoShop.Query.Query
{
    public class ProductSubCategoryQuery : IProductSubCategoryQuery
    {
        private readonly PsychoShopContext _context;

        public ProductSubCategoryQuery(PsychoShopContext context)
        {
            _context = context;
        }

        public async Task<ProductSubCategoryQueryModel> GetProductsWithProductSubCategory(string slug)
        {
            var inventory = await _context.Inventory
             .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _context.Discounts
                   .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                   .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var subCategory = await _context.ProductSubCategories
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductSubCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Products = MapProducts(x.Products)
                }).OrderByDescending(x => x.Id).AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug);

            foreach (var product in subCategory.Products)
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

            return subCategory;
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
    }
}