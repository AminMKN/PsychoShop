namespace PsychoShop.Application.Contracts.Product
{
    public class ProductSearchModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
