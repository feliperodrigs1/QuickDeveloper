using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickDeveloper.Models;

namespace QuickDeveloper.Controllers {

    public class ChatController : Controller {
        RouteValueDictionary routePost = new RouteValueDictionary { { "httpMethod", "POST" } };
        public IActionResult ChatBot() {
            var jwt = Request.Cookies["token"]?.ToString();

            if (!AuthenticateController.VerifyUser(Request, "Regular")) {
                return RedirectToAction("SignIn", "Register", routePost);
            }

            return View();
        }

        [HttpPost]
        public List<Model_View_User_Competences> ReturnUsersByCompetence(string userCompetence)
        {
            try
            {
                List<Model_View_User_Competences> listUsers = Model_DB.UserByCompetence(userCompetence);

                return listUsers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public bool RegisterRequisition(Model_Requisition requisition)
        {
            try
            {
                var id = AuthenticateController.Instance.claimsPrincipal.FindFirst("id").Value;

                requisition.idUser = Convert.ToInt32(id);

                bool registered = Model_DB.Register_Requisition(requisition);

                return registered;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
