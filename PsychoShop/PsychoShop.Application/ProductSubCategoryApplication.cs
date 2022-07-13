﻿using PsychoShop.Application.Contracts.ProductSubCategory;
using PsychoShop.Domain.ProductSubCategoryAgg;
using PsychoShop.Framework.Application;

namespace PsychoShop.Application
{
    public class ProductSubCategoryApplication : IProductSubCategoryApplication
    {
        private readonly IProductSubCategoryRepository _productSubCategoryRepository;

        public ProductSubCategoryApplication(IProductSubCategoryRepository productSubCategoryRepository)
        {
            _productSubCategoryRepository = productSubCategoryRepository;
        }

        public OperationResult Create(CreateProductSubCategory command)
        {
            var operation = new OperationResult();
            if (_productSubCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var slug = command.Slug.Slugify();
            var productSubCategory = new ProductSubCategory(command.Name, slug, command.Keywords, command.MetaDescription, command.ProductCategoryId);
            _productSubCategoryRepository.Create(productSubCategory);
            _productSubCategoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Edit(EditProductSubCategory command)
        {
            var operation = new OperationResult();
            var productSubCategory = _productSubCategoryRepository.Get(command.Id);
            if (productSubCategory == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (_productSubCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedInfo);

            var slug = command.Slug.Slugify();
            productSubCategory.Edit(command.Name, slug, command.Keywords, command.MetaDescription, command.ProductCategoryId);
            _productSubCategoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var productSubCategory = _productSubCategoryRepository.Get(id);
            if (productSubCategory == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            productSubCategory.Remove();
            _productSubCategoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var productSubCategory = _productSubCategoryRepository.Get(id);
            if (productSubCategory == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            productSubCategory.Restore();
            _productSubCategoryRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public EditProductSubCategory GetDetails(int id)
        {
            return _productSubCategoryRepository.GetDetails(id);
        }

        public async Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesList()
        {
            return await _productSubCategoryRepository.GetProductSubCategoriesList();
        }

        public async Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesJson(int id)
        {
            return await _productSubCategoryRepository.GetProductSubCategoriesJson(id);
        }

        public async Task<List<ProductSubCategoryViewModel>> Search(ProductSubCategorySearchModel searchModel)
        {
            return await _productSubCategoryRepository.Search(searchModel);
        }
    }
}
