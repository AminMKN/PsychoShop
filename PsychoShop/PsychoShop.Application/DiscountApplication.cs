using PsychoShop.Application.Contracts.Discount;
using PsychoShop.Domain.DiscountAgg;
using PsychoShop.Framework.Application;

namespace PsychoShop.Application
{
    public class DiscountApplication : IDiscountApplication
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountApplication(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public OperationResult Define(DefineDiscount command)
        {
            var operation = new OperationResult();
            if (_discountRepository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var discount = new Discount(command.DiscountRate, startDate, endDate, command.Reason, command.ProductId);
            _discountRepository.Create(discount);
            _discountRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Edit(EditDiscount command)
        {
            var operation = new OperationResult();
            var discount = _discountRepository.Get(command.Id);
            if (discount == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (_discountRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            discount.Edit(command.DiscountRate, startDate, endDate, command.Reason, command.ProductId);
            _discountRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public EditDiscount GetDetails(int id)
        {
            return _discountRepository.GetDetails(id);
        }

        public async Task<List<DiscountViewModel>> Search(DiscountSearchModel searchModel)
        {
            return await _discountRepository.Search(searchModel);
        }
    }
}