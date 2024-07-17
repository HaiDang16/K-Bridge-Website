using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace K_Bridge.Components
{
    public class AdminNavigationMenuViewComponent : ViewComponent
    {
        private IKBridgeRepository _repository;
        private IForumRepository _forumRepository;
        public AdminNavigationMenuViewComponent(IKBridgeRepository repository, IForumRepository forumRepository)
        {
            _repository = repository;
            _forumRepository = forumRepository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.NavForum = _forumRepository.GetForumWithTopics();
            return View();
        }
    }
}
