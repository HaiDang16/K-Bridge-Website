using K_Bridge.Models;

namespace K_Bridge.Helpers
{
    public static class NotificationHelper
    {
        public static string GetNotiTitleForAdmin(NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.NewUser:
                    return "Người dùng mới";
                case NotificationType.NewPost:
                    return "Bài viết mới";
                default:
                    return "Thông báo mới";
            }
        }
        public static string GetNotiMessageForAdmin(NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.NewUser:
                    return "Vừa có một người dùng đăng ký tài khoản mới.";
                case NotificationType.NewPost:
                    return "Có bài viết mới cần phê duyệt.";
                default:
                    return "Có thông báo mới.";
            }
        }
    }
}
