using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.Discount;
using PsychoShop.Domain.DiscountAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class DiscountRepository : RepositoryBase<int, Discount>, IDiscountRepository
    {
        private readonly PsychoShopContext _context;

        public DiscountRepository(PsychoShopContext context) : base(context)
        {
            _context = context;
        }

        public EditDiscount GetDetails(int id)
        {
            return _context.Discounts.Select(x => new EditDiscount()
            {
                Id = x.Id,
                Reason = x.Reason,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi()
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<DiscountViewModel>> Search(DiscountSearchModel searchModel)
        {
            var query = _context.Discounts.Select(x => new DiscountViewModel()
            {
                Id = x.Id,
                Reason = x.Reason,
                ProductId = x.ProductId,
                Product = x.Product.Name,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
