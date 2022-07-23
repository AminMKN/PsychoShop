namespace PsychoShop.Application.Contracts.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string UserAccountId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Product { get; set; }
        public string CreationDate { get; set; }
        public bool IsConfirmed { get; set; }
    }
}