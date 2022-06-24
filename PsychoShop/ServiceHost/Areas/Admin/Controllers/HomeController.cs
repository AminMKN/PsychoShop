using Microsoft.AspNetCore.Mvc;
using PsychoShop.Framework.Application;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
