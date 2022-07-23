using PsychoShop.Application.Contracts.Comment;
using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.CommentAgg
{
    public interface ICommentRepository : IRepositoryBase<int, Comment>
    {
        Task<List<CommentViewModel>> Search(CommentSearchModel searchModel);
    }
}