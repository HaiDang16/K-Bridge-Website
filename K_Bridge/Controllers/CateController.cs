using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    public class CateController : Controller
    {
        [Route("/Categories")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Newest/TopicList")]
        [Route("/Categories/TopicList")]
        public IActionResult TopicList()
        {
            return View();
        }

        [Route("/Newest/TopicList/Post/{postId}/{postTitle}")]
        [Route("/Categories/TopicList/Post/{postId}/{postTitle}")]
        public IActionResult Post(int postId, string postTitle)
        {
            postId = 123;
            postTitle = "TitlePost";
            return View();
        }
    }
}
