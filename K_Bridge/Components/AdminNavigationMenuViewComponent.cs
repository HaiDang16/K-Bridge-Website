using K_Bridge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace K_Bridge.Components
{
    public class AdminNavigationMenuViewComponent : ViewComponent
    {
        private IKBridgeRepository _repository;
        public AdminNavigationMenuViewComponent(IKBridgeRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
