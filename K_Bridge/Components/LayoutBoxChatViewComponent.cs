using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Components
{
    public class LayoutBoxChatViewComponent : ViewComponent
    {
        private IKBridgeRepository _repository;
        public LayoutBoxChatViewComponent(IKBridgeRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
