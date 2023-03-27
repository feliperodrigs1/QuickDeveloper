using Microsoft.AspNetCore.Mvc;
using QuickDeveloper.Models;
using System.Diagnostics;

namespace QuickDeveloper.Controllers {
    public class RegisterController : Controller {

        Model_User usuario = new Model_User();
        Model_DB dbase = new Model_DB();

        public IActionResult Register() {
            return View();
        }


        public IActionResult Competences(Model_User user) {
            return View(usuario);
        }


        [System.Web.Mvc.HttpPost]
        public IActionResult CompetencesPath() {
            var path = Url.Action("Competences", "Home");
            return Json(new { Path = path });
        }

        public IActionResult Login(Model_User user) {
            usuario = user;
            return View(usuario);
        }

        public IActionResult Cadastrar(Model_User user) {
            usuario = user;
            return RedirectToAction("Competences", "Register", usuario);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
