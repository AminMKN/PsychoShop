using PsychoShop.Application.Contracts.SpecialProduct;
using PsychoShop.Domain.SpecialProductAgg;
using PsychoShop.Framework.Application;

namespace PsychoShop.Application
{
    public class SpecialProductApplication : ISpecialProductApplication
    {
        private readonly ISpecialProductRepository _specialProductRepository;

        public SpecialProductApplication(ISpecialProductRepository specialProductRepository)
        {
            _specialProductRepository = specialProductRepository;
        }

        public OperationResult Create(CreateSpecialProduct command)
        {
            var operation = new OperationResult();
            if (_specialProductRepository.Exists(x => x.ProductId == command.ProductId && x.Type == command.Type))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var specialProduct = new SpecialProduct(startDate, endDate, command.Type, command.ProductId);
            _specialProductRepository.Create(specialProduct);
            _specialProductRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Edit(EditSpecialProduct command)
        {
            var operation = new OperationResult();
            var specialProduct = _specialProductRepository.Get(command.Id);
            if (specialProduct == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (_specialProductRepository.Exists(x => x.ProductId == command.ProductId && x.Type == command.Type && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            specialProduct.Edit(startDate, endDate, command.Type, command.ProductId);
            _specialProductRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public EditSpecialProduct GetDetails(int id)
        {
            return _specialProductRepository.GetDetails(id);
        }

        public async Task<List<SpecialProductViewModel>> Search(SpecialProductSearchModel searchModel)
        {
            return await _specialProductRepository.Search(searchModel);
        }
    }
}
