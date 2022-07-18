using PsychoShop.Application.Contracts.Discount;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.DiscountAgg
{
    public interface IDiscountRepository : IRepositoryBase<int, Discount>
    {
        EditDiscount GetDetails(int id);
        Task<List<DiscountViewModel>> Search(DiscountSearchModel searchModel);
    }
}
