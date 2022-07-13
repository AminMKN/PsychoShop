namespace PsychoShop.Application.Contracts.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string Picture { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}
