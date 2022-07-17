namespace PsychoShop.Application.Contracts.SpecialProduct
{
    public class SpecialProductViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Type { get; set; }
        public string Product { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreationDate { get; set; }
    }
}
