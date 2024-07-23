using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Repositories
{
    public class EFNotificationRepository : INotificationRepository
    {
        private KBridgeDbContext _context;
        public EFNotificationRepository(KBridgeDbContext context)
        {
            _context = context;
        }
        public IQueryable<Notification> Notifications => _context.Notifications;
        public void SendNotificationForUser(int userId, string title, string message, NotificationType notificationType)
        {
            var notification = new Notification
            {
                UserID = userId,
                Title = title,
                Message = message,
                NotiType = notificationType
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public void SendNotificationForAdmin(int adminId, string title, string message, NotificationType notificationType)
        {
            var notification = new Notification
            {
                AdminID = adminId,
                Title = title,
                Message = message,
                NotiType = notificationType
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public void SendNotificationForAllUsers(string title, string message, NotificationType notificationType)
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                var notification = new Notification
                {
                    UserID = user.ID,
                    Title = title,
                    Message = message,
                    NotiType = notificationType
                };
                _context.Notifications.Add(notification);
            }
            _context.SaveChanges();
        }

        public void SendNotificationForAllAdmins(string title, string message, NotificationType notificationType)
        {
            var admins = _context.Admin_Accounts.ToList();
            foreach (var admin in admins)
            {
                var notification = new Notification
                {
                    AdminID = admin.ID,
                    Title = title,
                    Message = message,
                    NotiType = notificationType
                };
                _context.Notifications.Add(notification);
            }
            _context.SaveChanges();
        }
        public List<Notification> GetAdminNotificationsById(int adminId)
        {
            return _context.Notifications
                .Where(n => n.AdminID == adminId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }

        public void MarkNotificationAsRead(int notificationId)
        {
            var notification = _context.Notifications.Find(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                _context.SaveChanges();
            }
        }
        public List<Notification> GetUserNotificationsById(int id)
        {
            return _context.Notifications
                .Where(n => n.UserID == id)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }

        public void SendNotificationForPostAuthor(int userId, int postId, string title,
            string message, NotificationType notificationType)
        {
            var notification = new Notification
            {
                UserID = userId,
                PostID = postId,
                Title = title,
                Message = message,
                NotiType = notificationType
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public void SendNotificationForReplyAuthor(int userId, int replyId, string title, string message, NotificationType notificationType)
        {
            var notification = new Notification
            {
                UserID = userId,
                ReplyID = replyId,
                Title = title,
                Message = message,
                NotiType = notificationType
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
        public void SendNotificationForVoteAuthor(int userId, int voteId, string title, string message, NotificationType notificationType)
        {
            var notification = new Notification
            {
                UserID = userId,
                VoteID = voteId,
                Title = title,
                Message = message,
                NotiType = notificationType
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();

        }
    }
}
