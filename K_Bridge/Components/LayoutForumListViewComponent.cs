using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Components
{
    public class LayoutForumListViewComponent : ViewComponent
    {
        private IForumRepository _repository;
        public LayoutForumListViewComponent(IForumRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Forums = _repository.Forums;
            return View();
        }
    }
}
