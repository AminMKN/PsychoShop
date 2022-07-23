using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychoShop.Application.Contracts.Comment;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize(Policy = "CommentPolicy")]
    public class CommentController : Controller
    {
        private readonly ICommentApplication _commentApplication;

        public CommentController(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(CommentSearchModel searchModel)
        {
            var command = new CommentAdminCommand()
            {
                Comments = await _commentApplication.Search(searchModel)
            };

            return View(command);
        }

        #endregion

        #region Remove-Restore

        public IActionResult Confirm(int id)
        {
            _commentApplication.Confirm(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cancel(int id)
        {
            _commentApplication.Cancel(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
