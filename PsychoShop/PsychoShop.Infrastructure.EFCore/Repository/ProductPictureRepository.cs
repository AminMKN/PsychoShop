using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.ProductPicture;
using PsychoShop.Domain.ProductPictureAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<int, ProductPicture>, IProductPictureRepository
    {
        private readonly PsychoShopContext _context;

        public ProductPictureRepository(PsychoShopContext context) : base(context)
        {
            _context = context;
        }

        public ProductPicture GetWithProduct(int id)
        {
            return _context.ProductPictures
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }

        public EditProductPicture GetDetails(int id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<ProductPictureViewModel>> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Select(x => new ProductPictureViewModel()
            {
                Id = x.Id,
                Picture = x.Picture,
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved,
                Product = x.Product.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });

            query = query.Where(x => x.IsRemoved == searchModel.IsRemoved);

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
