using PsychoShop.Application.Contracts.SpecialProduct;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.SpecialProductAgg
{
    public interface ISpecialProductRepository : IRepositoryBase<int, SpecialProduct>
    {
        EditSpecialProduct GetDetails(int id);
        Task<List<SpecialProductViewModel>> Search(SpecialProductSearchModel searchModel);
    }
}
