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
            var forums = _repository.GetForumWithTopics();
           
            return View();
        }
    }
}
