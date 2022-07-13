using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.ProductSubCategory;
using PsychoShop.Domain.ProductSubCategoryAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class ProductSubCategoryRepository : RepositoryBase<int, ProductSubCategory>, IProductSubCategoryRepository
    {
        private readonly PsychoShopContext _context;

        public ProductSubCategoryRepository(PsychoShopContext context) : base(context)
        {
            _context = context;
        }
        
        public EditProductSubCategory GetDetails(int id)
        {
            return _context.ProductSubCategories.Select(x => new EditProductSubCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                ProductCategoryId = x.ProductCategoryId
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesList()
        {
            return await _context.ProductSubCategories.Select(x => new ProductSubCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesJson(int id)
        {
            return await _context.ProductSubCategories
              .Where(x => x.ProductCategoryId == id)
              .Select(x => new ProductSubCategoryViewModel()
              {
                  Id = x.Id,
                  Name = x.Name
              }).AsNoTracking().ToListAsync();
        }

        public async Task<List<ProductSubCategoryViewModel>> Search(ProductSubCategorySearchModel searchModel)
        {
            var query = _context.ProductSubCategories.Select(x => new ProductSubCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.ToFarsi(),
                ProductCategoryId = x.ProductCategoryId,
                ProductCategory = x.ProductCategory.Name
            });

            query = query.Where(x => x.IsRemoved == searchModel.IsRemoved);

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (searchModel.ProductCategoryId != 0)
                query = query.Where(x => x.ProductCategoryId == searchModel.ProductCategoryId);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
