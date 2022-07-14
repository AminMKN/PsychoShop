using PsychoShop.Application.Contracts.ProductPicture;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepositoryBase<int, ProductPicture>
    {
        ProductPicture GetWithProduct(int id);
        EditProductPicture GetDetails(int id);
        Task<List<ProductPictureViewModel>> Search(ProductPictureSearchModel searchModel);
    }
}
