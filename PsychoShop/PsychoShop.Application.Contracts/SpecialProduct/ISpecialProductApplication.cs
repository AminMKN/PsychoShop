using PsychoShop.Framework.Application;

namespace PsychoShop.Application.Contracts.SpecialProduct
{
    public interface ISpecialProductApplication
    {
        OperationResult Create(CreateSpecialProduct command);
        OperationResult Edit(EditSpecialProduct command);
        EditSpecialProduct GetDetails(int id);
        Task<List<SpecialProductViewModel>> Search(SpecialProductSearchModel searchModel);
    }
}
