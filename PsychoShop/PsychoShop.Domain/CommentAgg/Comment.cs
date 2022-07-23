using PsychoShop.Domain.ProductAgg;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public string Message { get; set; }
        public string UserAccountId { get; set; }
        public int ProductId { get; set; }
        public bool IsConfirmed { get; set; }
        public Product Product { get; set; }

        public Comment(string message, string userAccountId, int productId)
        {
            UserAccountId = userAccountId;
            Message = message;
            ProductId = productId;
            IsConfirmed = false;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void Cancel()
        {
            IsConfirmed = false;
        }
    }
}
