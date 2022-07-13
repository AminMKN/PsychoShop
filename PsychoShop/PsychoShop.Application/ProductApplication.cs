using PsychoShop.Application.Contracts.Product;
using PsychoShop.Domain.ProductAgg;
using PsychoShop.Framework.Application;

namespace PsychoShop.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var slug = command.Slug.Slugify();
            var picturePath = $"{"Products"}/{slug}";
            var picture = _fileUploader.Upload(command.Picture, picturePath);
            var product = new Product(command.Name, slug, command.Code, command.Information, command.Property,
                command.Description, picture, command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, command.ProductCategoryId, command.ProductSubCategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(command.Id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var slug = command.Slug.Slugify();
            var picturePath = $"{"Products"}/{slug}";
            var picture = _fileUploader.Upload(command.Picture, picturePath);
            product.Edit(command.Name, slug, command.Code, command.Information, command.Property,
                command.Description, picture, command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, command.ProductCategoryId, command.ProductSubCategoryId);
            _productRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            product.Remove();
            _productRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            product.Restore();
            _productRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public EditProduct GetDetails(int id)
        {
            return _productRepository.GetDetails(id);
        }

        public async Task<List<ProductViewModel>> GetProductsList()
        {
            return await _productRepository.GetProductsList();
        }

        public async Task<List<ProductViewModel>> Search(ProductSearchModel searchModel)
        {
            return await _productRepository.Search(searchModel);
        }
    }
}
