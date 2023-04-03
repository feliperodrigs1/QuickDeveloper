using Microsoft.AspNetCore.Mvc;
using QuickDeveloper.Models;

namespace QuickDeveloper.Controllers {
    public class ChatController : Controller {
        public IActionResult ChatBot() {
            return View();
        }
    }
}
