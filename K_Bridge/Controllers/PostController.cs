using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    [Route("[controller]")]
    public class PostController : Controller
    {
        [HttpGet("Create")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
