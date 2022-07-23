using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.ShopCart;
using PsychoShop.Domain.ProductPictureAgg;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Infrastructure.EFCore;
using PsychoShop.Query.Contract.Comment;
using PsychoShop.Query.Contract.Product;
using PsychoShop.Query.Contract.ProductPicture;

namespace PsychoShop.Query.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly PsychoShopContext _context;
        private readonly IUserAccountRepository _userAccountRepository;

        public ProductQuery(PsychoShopContext context, IUserAccountRepository userAccountRepository)
        {
            _context = context;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<List<ProductQueryModel>> GetProductsList()
        {
            var inventory = await _context.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _context.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var products = await _context.Products
                 .Where(x => !x.IsRemoved)
                 .Select(x => new ProductQueryModel()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Slug = x.Slug,
                     Picture = x.Picture,
                     PictureAlt = x.PictureAlt,
                     PictureTitle = x.PictureTitle
                 }).OrderBy(x => Guid.NewGuid()).Take(20).AsNoTracking().ToListAsync();

            foreach (var product in products)
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

            return products;
        }

        public async Task<List<ProductQueryModel>> GetProductsHaveDiscount()
        {
            var inventory = await _context.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _context.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var products = await _context.Products
                 .Where(x => !x.IsRemoved)
                 .Select(x => new ProductQueryModel()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Slug = x.Slug,
                     Picture = x.Picture,
                     PictureAlt = x.PictureAlt,
                     PictureTitle = x.PictureTitle
                 }).AsNoTracking().ToListAsync();

            foreach (var product in products)
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

            return products.Where(x => x.HasDiscount).OrderByDescending(x => x.Id).ToList();
        }

        public async Task<List<ProductQueryModel>> GetSpecialProductsList(int type)
        {
            var inventory = await _context.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _context.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var products = await _context.Products
                .Where(x => !x.IsRemoved && x.SpecialProducts
                .Any(x => x.Type == type && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now))
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

            foreach (var product in products)
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

            return products;
        }

        public async Task<List<ProductQueryModel>> Search(string search)
        {
            var inventory = await _context.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _context.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var searchQuery = _context.Products
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                });

            if (!string.IsNullOrWhiteSpace(search))
                searchQuery = searchQuery.Where(x => x.Name.Contains(search) || x.Keywords.Contains(search));

            var products = await searchQuery.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

            foreach (var product in products)
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

            return products;
        }

        public async Task<ProductQueryModel> GetProductDetails(string slug)
        {
            var inventory = await _context.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _context.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var product = await _context.Products
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Code = x.Code,
                    Picture = x.Picture,
                    Property = x.Property,
                    Keywords = x.Keywords,
                    PictureAlt = x.PictureAlt,
                    Description = x.Description,
                    Information = x.Information,
                    PictureTitle = x.PictureTitle,
                    ProductCategory = x.ProductCategory.Name,
                    ProductSubCategory = x.ProductSubCategory.Name,
                    MetaDescription = x.MetaDescription,
                    Pictures = MapProductPictures(x.ProductPictures)
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug);

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

            var comments = await _context.Comments
                .Where(x => x.ProductId == product.Id && x.IsConfirmed)
                .Select(x => new CommentQueryModel()
                {
                    Id = x.Id,
                    UserAccountId = x.UserAccountId,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

            foreach (var comment in comments)
            {
                comment.UserName = await _userAccountRepository.GetUserName(comment.UserAccountId);
            }

            product.Comments = comments;

            return product;
        }

        public async Task<List<CartItem>> CheckInventoryStatus(List<CartItem> cartItems)
        {
            var inventory = await _context.Inventory.AsNoTracking().ToListAsync();

            foreach (var cartItem in cartItems.Where(x => inventory.Any(y => y.ProductId == x.Id && y.InStock)))
            {
                var itemInventory = inventory.Find(x => x.ProductId == cartItem.Id);
                cartItem.InStock = itemInventory.CalculateCurrentCount() >= cartItem.Count;
            }

            return cartItems;
        }

        private static List<ProductPictureQueryModel> MapProductPictures(IEnumerable<ProductPicture> productPictures)
        {
            return productPictures
               .Where(x => !x.IsRemoved)
               .Select(x => new ProductPictureQueryModel()
               {
                   Picture = x.Picture,
                   PictureAlt = x.PictureAlt,
                   PictureTitle = x.PictureTitle
               }).ToList();
        }
    }
}