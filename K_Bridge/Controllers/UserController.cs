using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    public class UserController : Controller
    {
        [Route("/UserProfile/{UserID}")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/UserProfile/EditProfile/{UserID}")]
        public IActionResult EditProfile()
        {
            return View();
        }

        [Route("/UserProfile/EditDisplayName/{UserID}")]
        public IActionResult EditDisplayName()
        {
            return View();
        }

        [Route("/UserProfile/EditUserName/{UserID}")]
        public IActionResult EditUserName()
        {
            return View();
        }

        [Route("/UserProfile/EditEmail/{UserID}")]
        public IActionResult EditEmail()
        {
            return View();
        }

        [Route("/UserProfile/EditPhoneNum/{UserID}")]
        public IActionResult EditPhoneNum()
        {
            return View();
        }

        [Route("/UserProfile/EditPass/{UserID}")]
        public IActionResult EditPass()
        {
            return View();
        }
    }
}