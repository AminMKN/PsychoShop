namespace PsychoShop.Application.Contracts.ProductSubCategory
{
    public class ProductSubCategoryViewModel
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string ProductCategory { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}
