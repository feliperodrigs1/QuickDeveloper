using Microsoft.AspNetCore.Mvc;
using QuickDeveloper.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using System.Web;

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

        [Route("Home/Redirect")]
        public async Task<IActionResult> Redirect(string id,string code)
        {
            string decodedInput = HttpUtility.UrlEncode(code);
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://164.152.196.151/ativa?UsuarioId={id}&CodigoDeAtivacao={decodedInput}");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                
                return View();
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        //[Authorize(Roles = "admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {            
            HttpContext.Response.Cookies.Delete("token");

            return RedirectToAction("Index", "Home", routePost);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}