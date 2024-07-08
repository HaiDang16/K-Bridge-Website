using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
