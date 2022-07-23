namespace PsychoShop.Query.Contract.Comment
{
    public class CommentQueryModel
    {
        public int Id { get; set; }
        public string UserAccountId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string CreationDate { get; set; }
    }
}
