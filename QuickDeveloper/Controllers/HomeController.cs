using Microsoft.AspNetCore.Mvc;
using QuickDeveloper.Models;
using System.Diagnostics;
using System.Web.Mvc;

namespace QuickDeveloper.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Competences()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public IActionResult CompetencesPath()
        {
            var path = Url.Action("Competences", "Home");
            return Json(new { Path = path });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}