using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using QuickDeveloper.Models;
using System.Diagnostics;

namespace QuickDeveloper.Controllers
{
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        RouteValueDictionary routePost = new RouteValueDictionary { { "httpMethod", "POST" } };
        public IActionResult Home()
        {
            Model_User user = JsonConvert.DeserializeObject<Model_User>((string)TempData["User"]);

            return View(user);
        }
        public IActionResult ShowDataUser()
        {
            var jwt = Request.Cookies["token"]?.ToString();

            if (AuthenticateController.VerifyUser(Request))
            {
                return View();
            }

            return RedirectToAction("SignIn", "Register", routePost);
        }

        public IActionResult EditDataUser(Model_View_User user)
        {
            try
            {
                Model_DB.UpdateData_User(user);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao alterar as informações!";

            }

            return View("ShowDataUser");


        }
    }
}

