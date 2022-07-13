namespace PsychoShop.Application.Contracts.ProductSubCategory
{
    public class ProductSubCategorySearchModel
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public bool IsRemoved { get; set; }
    }
}
