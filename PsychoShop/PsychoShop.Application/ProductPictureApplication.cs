using PsychoShop.Application.Contracts.ProductPicture;
using PsychoShop.Domain.ProductAgg;
using PsychoShop.Domain.ProductPictureAgg;
using PsychoShop.Framework.Application;

namespace PsychoShop.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(command.ProductId);
            if (product == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            var picturePath = $"{"Products"}/{product.Slug}";
            var picture = _fileUploader.Upload(command.Picture, picturePath);
            var productPicture = new ProductPicture(picture, command.PictureAlt, command.PictureTitle, command.ProductId);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetWithProduct(command.Id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            var picturePath = $"{"Products"}/{productPicture.Product.Slug}";
            var picture = _fileUploader.Upload(command.Picture, picturePath);
            productPicture.Edit(picture, command.PictureAlt, command.PictureTitle, command.ProductId);
            _productPictureRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public EditProductPicture GetDetails(int id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public async Task<List<ProductPictureViewModel>> Search(ProductPictureSearchModel searchModel)
        {
            return await _productPictureRepository.Search(searchModel);
        }
    }
}