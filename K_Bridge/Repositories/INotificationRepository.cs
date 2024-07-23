using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface INotificationRepository
    {
        IQueryable<Notification> Notifications { get; }
        void SendNotificationForAdmin(int adminId, string title, string message, NotificationType notificationType);
        void SendNotificationForUser(int userId, string title, string message, NotificationType notificationType);
        void SendNotificationForAllUsers(string title, string message, NotificationType notificationType);
        void SendNotificationForAllAdmins(string title, string message, NotificationType notificationType);
        List<Notification> GetAdminNotificationsById(int adminId);
        void MarkNotificationAsRead(int notificationId);
        List<Notification> GetUserNotificationsById(int id);
        void SendNotificationForPostAuthor(int userId, int postId, string title, string message, NotificationType notificationType);
        void SendNotificationForReplyAuthor(int userId, int replyId, string title, string message, NotificationType notificationType);
        void SendNotificationForVoteAuthor(int userId, int voteId, string title, string message, NotificationType notificationType);

    }
}
