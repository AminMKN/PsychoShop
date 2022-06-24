using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Domain.ProductCategoryAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<int, ProductCategory>, IProductCategoryRepository
    {
        private readonly PsychoShopContext _context;

        public ProductCategoryRepository(PsychoShopContext context) : base(context)
        {
            _context = context;
        }

        public string GetProductCategorySlug(int id)
        {
            return _context.ProductCategories
                 .Select(x => new { x.Id, x.Slug })
                 .AsNoTracking().FirstOrDefault(x => x.Id == id)?.Slug;
        }

        public EditProductCategory GetDetails(int id)
        {
            return _context.ProductCategories.Select(x => new EditProductCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription
            }).FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<ProductCategoryViewModel>> GetProductCategoriesList()
        {
            return await _context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<ProductCategoryViewModel>> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.ToFarsi()
            });

            query = query.Where(x => x.IsRemoved == searchModel.IsRemoved);

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
