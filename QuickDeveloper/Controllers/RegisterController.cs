using Microsoft.AspNetCore.Mvc;
using QuickDeveloper.Models;
using System.Diagnostics;

namespace QuickDeveloper.Controllers {
    public class RegisterController : Microsoft.AspNetCore.Mvc.Controller
    {

        public IActionResult Register() {
            return View();
        }

        public IActionResult Competences(Model_User user) {
            return View(user);
        }

        public IActionResult Login(Model_User user) {          
            return View(user);
        }

        [HttpPost]
        public IActionResult SignUp(Model_User user, IFormCollection form) {
            bool dev = !string.IsNullOrEmpty(form["dev"]);
            
            if (dev) return RedirectToAction("Competences", "Register", user);

            return RedirectToAction("Login", "Register", user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
