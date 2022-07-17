using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.SpecialProduct;
using PsychoShop.Domain.SpecialProductAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class SpecialProductRepository : RepositoryBase<int, SpecialProduct>, ISpecialProductRepository
    {
        private readonly PsychoShopContext _context;

        public SpecialProductRepository(PsychoShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSpecialProduct GetDetails(int id)
        {
            return _context.SpecialProducts.Select(x => new EditSpecialProduct()
            {
                Id = x.Id,
                Type = x.Type,
                ProductId = x.ProductId,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi()
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<SpecialProductViewModel>> Search(SpecialProductSearchModel searchModel)
        {
            var query = _context.SpecialProducts.Select(x => new SpecialProductViewModel()
            {
                Id = x.Id,
                Type = x.Type,
                ProductId = x.ProductId,
                Product = x.Product.Name,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.Type != 0)
                query = query.Where(x => x.Type == searchModel.Type);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
