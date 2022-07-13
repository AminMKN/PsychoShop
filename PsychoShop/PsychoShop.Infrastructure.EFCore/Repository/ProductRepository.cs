using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.Product;
using PsychoShop.Domain.ProductAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<int, Product>, IProductRepository
    {
        private readonly PsychoShopContext _context;

        public ProductRepository(PsychoShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(int id)
        {
            return _context.Products.Select(x => new EditProduct()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Slug = x.Slug,
                Keywords = x.Keywords,
                Property = x.Property,
                PictureAlt = x.PictureAlt,
                Description = x.Description,
                Information = x.Information,
                PictureTitle = x.PictureTitle,
                MetaDescription = x.MetaDescription,
                ProductCategoryId = x.ProductCategoryId,
                ProductSubCategoryId = x.ProductSubCategoryId
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<ProductViewModel>> GetProductsList()
        {
            return await _context.Products.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<ProductViewModel>> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Picture = x.Picture,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.ToFarsi(),
                ProductCategoryId = x.ProductCategoryId,
                ProductCategory = x.ProductCategory.Name,
                ProductSubCategoryId = x.ProductSubCategoryId,
                ProductSubCategory = x.ProductSubCategory.Name,
            });

            query = query.Where(x => x.IsRemoved == searchModel.IsRemoved);

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            if (searchModel.ProductCategoryId != 0)
                query = query.Where(x => x.ProductCategoryId == searchModel.ProductCategoryId);

            if (searchModel.ProductSubCategoryId != 0)
                query = query.Where(x => x.ProductSubCategoryId == searchModel.ProductSubCategoryId);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
