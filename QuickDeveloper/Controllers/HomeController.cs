using Microsoft.AspNetCore.Mvc;
using QuickDeveloper.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;


namespace QuickDeveloper.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient client = new HttpClient();
        RouteValueDictionary routePost = new RouteValueDictionary { { "httpMethod", "POST" } };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Redirect()
        {
            return View();
        }

        //[Authorize(Roles = "admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {    
            var request = new HttpRequestMessage(HttpMethod.Post, "http://164.152.196.151/logout");
            var content = new StringContent("", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index", "Home", routePost);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}