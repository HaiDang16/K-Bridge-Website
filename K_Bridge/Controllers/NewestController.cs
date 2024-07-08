using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    public class NewestController : Controller
    {
        [Route("/Newest")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
