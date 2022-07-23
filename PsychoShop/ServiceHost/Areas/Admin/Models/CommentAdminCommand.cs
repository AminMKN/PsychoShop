using PsychoShop.Application.Contracts.Comment;

namespace ServiceHost.Areas.Admin.Models
{
    public class CommentAdminCommand
    {
        public AddComment AddComment { get; set; }
        public CommentSearchModel SearchModel { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}