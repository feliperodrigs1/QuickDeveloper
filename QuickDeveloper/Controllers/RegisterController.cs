using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuickDeveloper.Models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;


namespace QuickDeveloper.Controllers
{
    public class RegisterController : Microsoft.AspNetCore.Mvc.Controller
    {

        //Definindo uma variavel padrão para utilizar requisições POST de um Controller para outro
        RouteValueDictionary routePost = new RouteValueDictionary { { "httpMethod", "POST" } };

        HttpClient client = new HttpClient();

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Competences()
        {
            Model_User user = deserializeUser(TempData["User"]);
            TempData["User"] = JsonConvert.SerializeObject(user);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Model_User user)
        {
            TempData["User"] = JsonConvert.SerializeObject(user);            

            var request = new HttpRequestMessage(HttpMethod.Post, "http://164.152.196.151/login");

            var json = JsonConvert.SerializeObject(new
            {
                Email = user.Email,
                Password = user.Password
            });

            var content = new StringContent(json, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode) 
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                JArray jArray = JArray.Parse(responseContent);

                string token = jArray[0]["message"].ToString();

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(1),
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                };

                Response.Cookies.Append("token", token, cookieOptions);

                response.EnsureSuccessStatusCode();

                return RedirectToAction("Requisitions", "User", routePost);
            }
            else
            {
                TempData["Error"] = "Email ou/e Senha inválido(s). Tente novamente!";
                return RedirectToAction("SignIn", "Register", routePost);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(Model_User user, IFormCollection form)
        {
            TempData["User"] = JsonConvert.SerializeObject(user);

            bool dev = !string.IsNullOrEmpty(form["dev"]);

            if (dev) return RedirectToAction("Competences", "Register", routePost);

            var request = new HttpRequestMessage(HttpMethod.Post, "http://164.152.196.151/cadastro");

            user.Role = "2";
            user.RePassword = user.Password;
            var json = JsonConvert.SerializeObject(user);          

            var content = new StringContent(json, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                TempData["Email"] = "Verifique seu e-mail para confirmar o cadastro!";
                return RedirectToAction("Index", "Home", routePost);
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                JArray jArray = JArray.Parse(responseContent);

                TempData["Error"] = jArray[0]["message"].ToString();
                return RedirectToAction("SignIn", "Register", routePost);
            }
        }


        [HttpPost]
        public async Task<IActionResult> RegisterDev(string competences, string aditionalInfo)
        {
            Model_User user = deserializeUser(TempData["User"]);

            TempData["User"] = JsonConvert.SerializeObject(user);

            var request = new HttpRequestMessage(HttpMethod.Post, "http://164.152.196.151/cadastro");

            user.Competences = competences;
            user.AditionalInfo = aditionalInfo;
            user.Role = "3";
            user.RePassword = user.Password;          

            var json = JsonConvert.SerializeObject(user);

            var content = new StringContent(json, null, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            TempData["Email"] = "Verifique seu e-mail para confirmar o cadastro!";
            var path = Url.Action("Index", "Home");
            return Json(new { Path = path });
        }

        internal Model_User deserializeUser(dynamic tempData) 
        {
            return JsonConvert.DeserializeObject<Model_User>((string)tempData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
