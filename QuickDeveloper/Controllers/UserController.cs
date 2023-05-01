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

        public IActionResult Edit()
        {
            var jwt = Request.Cookies["token"]?.ToString();

            if (AuthenticateController.VerifyUser(Request))
            {
                return View();
            }

            return RedirectToAction("SignIn", "Register", routePost);
        }

        public IActionResult Requisitions()
        {
            return View();
        }

        [HttpPost]
        public Model_View_User FindUserByEmail(string email)
        {
            Model_View_User dataUser = Model_DB.UserRequisition(email);
            return dataUser;   
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

            return View("Edit");
        }
    }
}

