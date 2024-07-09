using K_Bridge.Infrastructure;
using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Components
{
    public class LayoutHeaderViewComponent : ViewComponent
    {
        private IKBridgeRepository _repository;
        public LayoutHeaderViewComponent(IKBridgeRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            User? user = HttpContext.Session.GetJson<User>("user");

            ViewBag.UsernameLoggedIn = "Tên tài khoản";
            if (user != null)
                ViewBag.UsernameLoggedIn = user.Username;

            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            return View();
        }
    }
}
