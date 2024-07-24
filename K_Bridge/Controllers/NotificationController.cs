using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    [Route("[controller]")]
    public class NotificationController : Controller
    {
        private readonly IKBridgeRepository _repository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(IKBridgeRepository repository, INotificationRepository notificationRepository)
        {
            _repository = repository;
            _notificationRepository = notificationRepository;
        }

        [HttpPost("MarkAsRead")]
        public IActionResult MarkAsRead(int id)
        {
            _notificationRepository.MarkNotificationAsRead(id);
            return Json(new { success = true });
        }
    }
}
