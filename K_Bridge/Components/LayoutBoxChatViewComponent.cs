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
            ViewBag.GlobalChats = _repository.GlobalChats;
            return View();
        }
    }
}
