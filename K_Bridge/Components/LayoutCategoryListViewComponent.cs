using K_Bridge.Models;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Components
{
    public class LayoutCategoryListViewComponent : ViewComponent
    {
        private IKBridgeRepository _repository;
        public LayoutCategoryListViewComponent(IKBridgeRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Categories = _repository.Categories;
            return View();
        }
    }
}
