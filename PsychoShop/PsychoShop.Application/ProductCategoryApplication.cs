using PsychoShop.Application.Contracts.ProductCategory;
using PsychoShop.Domain.ProductCategoryAgg;
using PsychoShop.Framework.Application;

namespace PsychoShop.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, slug, command.Keywords, command.MetaDescription);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name, slug, command.Keywords, command.MetaDescription);
            _productCategoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(id);
            if (productCategory == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            productCategory.Remove();
            _productCategoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(id);
            if (productCategory == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            productCategory.Restore();
            _productCategoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public EditProductCategory GetDetails(int id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public async Task<List<ProductCategoryViewModel>> GetProductCategoriesList()
        {
            return await _productCategoryRepository.GetProductCategoriesList();
        }

        public async Task<List<ProductCategoryViewModel>> Search(ProductCategorySearchModel searchModel)
        {
            return await _productCategoryRepository.Search(searchModel);
        }
    }
}
