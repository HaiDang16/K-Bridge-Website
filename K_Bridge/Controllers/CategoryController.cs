using K_Bridge.Models;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        [Route("List")]
        public IActionResult Index(string forum)
        {
            ViewBag.ForumName = forum;
            return View();
        }

        [Route("/Newest/TopicList")]
        [Route("TopicList")]
        public IActionResult TopicList()
        {
            return View();
        }

        [Route("TopicList/Post")]
        public IActionResult Post(int postId, string postTitle)
        {
            postId = 123;
            postTitle = "TitlePost";
            return View();
        }
    }
}
