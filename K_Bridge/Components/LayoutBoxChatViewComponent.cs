using K_Bridge.Infrastructure;
using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Components
{
    public class LayoutBoxChatViewComponent : ViewComponent
    {
        private IGlobalChatRepository _repository;
        public LayoutBoxChatViewComponent(IGlobalChatRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.GlobalChats = _repository.GetRecentMessages().ToList();

            User? user = HttpContext.Session.GetJson<User>("user");
            if (user == null)
                ViewBag.Username = "User";
            else
            {
                ViewBag.Username = user.Username;
                ViewBag.Avatar = user.Avatar;
            }
            return View();
        }
    }
}
