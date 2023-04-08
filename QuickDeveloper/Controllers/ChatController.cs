using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickDeveloper.Models;

namespace QuickDeveloper.Controllers {
    
    
    public class ChatController : Controller {
        RouteValueDictionary routePost = new RouteValueDictionary { { "httpMethod", "POST" } };
        public IActionResult ChatBot() {
            var jwt = Request.Cookies["token"]?.ToString();

            if (!AuthenticateController.VerifyUser(Request,"Regular"))
            {
                return RedirectToAction("SignIn", "Register", routePost);
            };

            return View();
        }
    }
}
