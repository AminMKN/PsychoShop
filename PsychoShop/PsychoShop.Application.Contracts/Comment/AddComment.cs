namespace PsychoShop.Application.Contracts.Comment
{
    public class AddComment
    {
        public string Message { get; set; }
        public string UserAccountId { get; set; }
        public int ProductId { get; set; }
    }
}
