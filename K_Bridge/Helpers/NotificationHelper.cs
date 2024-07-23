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
        public static string GetNotiTitleForUser(NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.PostRejected:
                    return "Bài viết bị từ chối";
                case NotificationType.PostApproved:
                    return "Bài viết được duyệt";
                case NotificationType.PostBlocked:
                    return "Bài viết bị khoá";

                default:
                    return "Thông báo mới";
            }
        }
        public static string GetNotiMessageForUser(NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.PostRejected:
                    return "Bài viết của bạn đã bị từ chối. Kiểm tra kỹ các thông tin liên quan để bài viết hợp lệ!!";
                case NotificationType.PostApproved:
                    return "Bài viết của bạn đã được phê duyệt. Hãy đến và cùng bình luận với mọi người nào!!!";
                case NotificationType.PostBlocked:
                    return "Bài viết của bạn đã bị khoá do vi phạm tiêu chuẩn cộng đồng. Mọi thắc mắc hãy liên hệ admin để được giải quyết.";

                default:
                    return "Có thông báo mới.";
            }
        }
    }
}
