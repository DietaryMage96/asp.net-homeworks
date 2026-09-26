using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dz1_ASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Hobbies()
        {
            return View();
        }
        public IActionResult Favorites()
        {
            return View();
        }
        public IActionResult Plans()
        {
            return View();
        }
    }
}
