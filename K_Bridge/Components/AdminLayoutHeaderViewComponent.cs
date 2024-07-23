using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Components
{
    public class AdminLayoutHeaderViewComponent : ViewComponent
    {
        private IKBridgeRepository _repository;
        private INotificationRepository _notificationRepository;
        public AdminLayoutHeaderViewComponent(IKBridgeRepository repository, INotificationRepository notificationRepository)
        {
            _repository = repository;
            _notificationRepository = notificationRepository;
        }

        public IViewComponentResult Invoke()
        {
            int adminId = HttpContext.Session.GetInt32("AdminID").GetValueOrDefault();
            var notifications = _notificationRepository.GetAdminNotificationsById(adminId);
            ViewBag.Notifications = notifications;
            ViewBag.UnreadCount = notifications.Count(n => !n.IsRead);
            return View();
        }
    }
}
