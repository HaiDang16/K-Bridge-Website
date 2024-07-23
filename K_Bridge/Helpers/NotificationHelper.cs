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
                case NotificationType.NewReply:
                    return "Bình luận mới";
                case NotificationType.NewLike:
                    return "Lượt thích bài viết mới";
                case NotificationType.NewDisLike:
                    return "Lượt không thích bài viết mới";
                case NotificationType.NewLikeReply:
                    return "Lượt thích bình luận mới";
                case NotificationType.NewDislikeReply:
                    return "Lượt không thích bình luận mới";
                case NotificationType.NewVote:
                    return "Lượt bình chọn mới";

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
                case NotificationType.NewReply:
                    return "vừa thêm một bình luận vào bài viết của bạn. Hãy đến và xem nào."; //+ tên đằng trước
                case NotificationType.NewLike:
                    return "Có một lượt thích mới trên bài viết của bạn. Tiếp tục phát huy nhé!!!";
                case NotificationType.NewDisLike:
                    return "Ai đó đã không thích bài viết của bạn. Hãy đến bài viết và xem có vấn đề không nhé!!!";
                case NotificationType.NewLikeReply:
                    return "Có một lượt thích mới trên bình luận của bạn. Tiếp tục phát huy nhé!!!";
                case NotificationType.NewDislikeReply:
                    return "Ai đó đã không thích bình luận của bạn. Hãy đến bình luận và xem có vấn đề không nhé!!!";
                case NotificationType.NewVote:
                    return "Vừa có một lượt bình chọn trên bình chọn của bạn. Hãy đến và xem nào!!!";

                default:
                    return "Có thông báo mới.";
            }
        }
    }
}
