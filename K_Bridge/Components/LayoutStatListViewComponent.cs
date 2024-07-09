using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Components
{
    public class LayoutStatListViewComponent : ViewComponent
    {
        private IKBridgeRepository _repository;
        public LayoutStatListViewComponent(IKBridgeRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Statses = _repository.Statses;
            return View();
        }
    }
}
