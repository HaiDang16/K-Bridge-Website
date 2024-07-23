namespace K_Bridge.Models
{
    public enum NotificationType
    {
        PostApproval,
        NewUser,
        Periodic,
        Global,
        PostUpdate,
        NewPost
    }

    public class Notification : BaseModel
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; } = false;

        // Foreign key
        public int? UserID { get; set; }
        public User? User { get; set; }

        public int? PostID { get; set; }
        public Post? Post { get; set; }

        public int AdminID { get; set; }
        public Admin_Accounts? Admin { get; set; }
        public int? ReplyID { get; set; }
        public Reply? Reply { get; set; }

        public NotificationType NotiType { get; set; }
    }
}
