using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuickDeveloper.Models;
using System.Diagnostics;

namespace QuickDeveloper.Controllers {
    public class RegisterController : Microsoft.AspNetCore.Mvc.Controller
    {
        //Definindo uma variavel padrão para utilizar requisições POST de um Controller para outro
        RouteValueDictionary routePost = new RouteValueDictionary { { "httpMethod", "POST" } };

        public IActionResult SignIn() {
            return View();
        }

        public IActionResult Competences() {
            Model_User user = deserializeUser(TempData["User"]);
            TempData["User"] = JsonConvert.SerializeObject(user);

            return View(user);
        }

        [HttpPost]
        public IActionResult Login(Model_User user) {
            //TODO chamada da API

            TempData["User"] = JsonConvert.SerializeObject(user);

            return RedirectToAction("Home", "User", routePost);
        }

        [HttpPost]
        public IActionResult SignUp(Model_User user, IFormCollection form) {
            bool dev = !string.IsNullOrEmpty(form["dev"]);

            TempData["User"] = JsonConvert.SerializeObject(user);

            if (dev) return RedirectToAction("Competences", "Register", routePost);

            //TODO chamada da API para Solicitante
            return RedirectToAction("Home", "User", routePost);
        }

        [System.Web.Mvc.HttpPost]
        public IActionResult RegisterDev(string competences, string aditionalInfo)
        {
            Model_User user = deserializeUser(TempData["User"]);
            user.Competences = competences;
            user.AditionalInfo = aditionalInfo;

            TempData["User"] = JsonConvert.SerializeObject(user);

            //TODO chamada da API para Dev

            var path = Url.Action("Home", "User");
            return Json(new { Path = path });
        }

        internal Model_User deserializeUser(dynamic tempData)
        {
            return JsonConvert.DeserializeObject<Model_User>((string)tempData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
