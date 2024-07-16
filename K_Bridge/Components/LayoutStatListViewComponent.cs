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
            int totalUsers = _repository.Users.Count();

            int totalTopics = _repository.Topics.Count();
            totalTopics = totalTopics < 10 ? 10 : (totalTopics / 10) * 10;

            int totalPosts = _repository.Posts.Count();

            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalTopics = totalTopics;
            ViewBag.TotalPosts = totalPosts;
            return View();
        }
    }
}
