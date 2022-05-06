using Microsoft.AspNetCore.Mvc;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;
using System.Diagnostics;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
