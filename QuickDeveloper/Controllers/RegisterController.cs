using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuickDeveloper.Models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace QuickDeveloper.Controllers {
    public class RegisterController : Microsoft.AspNetCore.Mvc.Controller
    {
        //Definindo uma variavel padrão para utilizar requisições POST de um Controller para outro
        RouteValueDictionary routePost = new RouteValueDictionary { { "httpMethod", "POST" } };

        HttpClient client = new HttpClient();

        public IActionResult SignIn() {
            return View();
        }

        public IActionResult Competences() {
            Model_User user = deserializeUser(TempData["User"]);
            TempData["User"] = JsonConvert.SerializeObject(user);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Model_User user) {            
            TempData["User"] = JsonConvert.SerializeObject(user);            

            var request = new HttpRequestMessage(HttpMethod.Post, "http://164.152.196.151/login");           
            var content = new StringContent($"{{\r\n \"Email\":\"{user.Email}\",\r\n    \"Password\" : \"{user.Password}\"\r\n}}", null, "application/json");
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
                return RedirectToAction("Home", "User", routePost);
            }
            else
            {               
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["Error"] = "Email ou/e Senha invalido(s). Tente novamente!";
                return RedirectToAction("SignIn", "Register", routePost);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(Model_User user, IFormCollection form) {
            bool dev = !string.IsNullOrEmpty(form["dev"]);

            TempData["User"] = JsonConvert.SerializeObject(user);

            string formattedUserDateTime = user.Birthdate.ToString("yyyy-MM-ddTHH:mm:ss");

            if (dev) return RedirectToAction("Competences", "Register", routePost);

            var request = new HttpRequestMessage(HttpMethod.Post, "http://164.152.196.151/cadastro");
            var content = new StringContent(
                $"{{\r\n    \"Username\" : \"{user.Username}\",\r\n    " +
                $"\"Email\":\"{user.Email}\",\r\n    " +
                $"\"Password\" : \"{user.Password}\",\r\n    " +
                $"\"RePassword\" : \"{user.Password}\",\r\n    " +
                $"\"DataNascimento\" : \"{formattedUserDateTime}\",\r\n    " +
                $"\"Role\" : \"2\"\r\n}}", null, "application/json");

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            TempData["Email"] = "Verifique seu e-mail para confirmar o cadastro!";
            return RedirectToAction("Index", "Home", routePost);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<IActionResult> RegisterDev(string competences, string aditionalInfo)
        {
            Model_User user = deserializeUser(TempData["User"]);            

            TempData["User"] = JsonConvert.SerializeObject(user);

            string formattedUserDateTime = user.Birthdate.ToString("yyyy-MM-ddTHH:mm:ss");

            var request = new HttpRequestMessage(HttpMethod.Post, "http://164.152.196.151/cadastro");
            var content = new StringContent($"{{\r\n    \"Username\" : \"{user.Username}\",\r\n    " +
                $"\"Email\":\"{user.Email}\",\r\n    " +
                $"\"Password\" : \"{user.Password}\",\r\n    " +
                $"\"RePassword\" : \"{user.Password}\",\r\n    " +
                $"\"DataNascimento\" : \"{formattedUserDateTime}\",\r\n    " +
                $"\"Role\" : \"3\",\r\n   " +
                $"\"Competencias\" : \"{competences}\", \r\n    " + 
                $"\"InfoAdicionais\" : \"{aditionalInfo}\"\r\n}}", null, "application/json");

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
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
