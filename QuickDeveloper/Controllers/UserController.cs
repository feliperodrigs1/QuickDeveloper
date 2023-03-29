using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using QuickDeveloper.Models;
using System.Diagnostics;

namespace QuickDeveloper.Controllers
{
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Home()
        { 
            Model_User user = JsonConvert.DeserializeObject<Model_User>((string)TempData["User"]);

            return View(user);
        }
    }
}

