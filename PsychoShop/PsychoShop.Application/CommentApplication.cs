using PsychoShop.Application.Contracts.Comment;
using PsychoShop.Domain.CommentAgg;
using PsychoShop.Framework.Application;

namespace PsychoShop.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation = new OperationResult();
            var comment = new Comment( command.Message, command.UserAccountId, command.ProductId);
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return operation.Success(ApplicationMessages.CommentSuccess);
        }

        public OperationResult Confirm(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            comment.Confirm();
            _commentRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Cancel(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            comment.Cancel();
            _commentRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public async Task<List<CommentViewModel>> Search(CommentSearchModel searchModel)
        {
            return await _commentRepository.Search(searchModel);
        }
    }
}
