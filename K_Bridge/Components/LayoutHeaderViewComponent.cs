using K_Bridge.Infrastructure;
using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Components
{
    public class LayoutHeaderViewComponent : ViewComponent
    {
        private IKBridgeRepository _repository;
        private INotificationRepository _notificationRepository;

        public LayoutHeaderViewComponent(IKBridgeRepository repository, INotificationRepository notificationRepository)
        {
            _repository = repository;
            _notificationRepository = notificationRepository;
        }

        public IViewComponentResult Invoke()
        {
            User? user = HttpContext.Session.GetJson<User>("user");

            ViewBag.UsernameLoggedIn = "Tên tài khoản";
            ViewBag.UserID = 0;

            if (user != null)
            {
                ViewBag.UsernameLoggedIn = user.Username;
                ViewBag.UserID = user.ID;
                var notifications = _notificationRepository.GetUserNotificationsById(user.ID);
                ViewBag.Notifications = notifications;
                ViewBag.UnreadCount = notifications.Count(n => !n.IsRead);

            }

            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;


            return View();
        }
    }
}
