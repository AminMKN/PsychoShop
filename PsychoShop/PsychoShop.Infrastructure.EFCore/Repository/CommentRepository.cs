using Microsoft.EntityFrameworkCore;
using PsychoShop.Application.Contracts.Comment;
using PsychoShop.Domain.CommentAgg;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Infrastructure;

namespace PsychoShop.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<int, Comment>, ICommentRepository
    {
        private readonly PsychoShopContext _context;
        private readonly IUserAccountRepository _userAccountRepository;

        public CommentRepository(PsychoShopContext context, IUserAccountRepository userAccountRepository) : base(context)
        {
            _context = context;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<List<CommentViewModel>> Search(CommentSearchModel searchModel)
        {
            var comments = await _context.Comments.Select(x => new CommentViewModel()
            {
                Id = x.Id,
                Message = x.Message,
                Product = x.Product.Name,
                IsConfirmed = x.IsConfirmed,
                UserAccountId = x.UserAccountId,
                CreationDate = x.CreationDate.ToFarsi(),
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

            foreach (var comment in comments)
            {
                comment.UserName = await _userAccountRepository.GetUserName(comment.UserAccountId);
            }

            return comments.Where(x => x.IsConfirmed == searchModel.IsConfirmed).ToList();
        }
    }
}