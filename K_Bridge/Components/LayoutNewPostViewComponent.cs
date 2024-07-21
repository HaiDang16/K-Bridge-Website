using K_Bridge.Infrastructure;
using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Components
{
    public class LayoutNewPostViewComponent : ViewComponent
    {
        private IForumRepository _repository;
        public LayoutNewPostViewComponent(IForumRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.VCForums = _repository.GetForumWithTopics();

            User? user = HttpContext.Session.GetJson<User>("user");

            ViewBag.IsLoggedIn = user != null;

            return View();
        }
    }
}
