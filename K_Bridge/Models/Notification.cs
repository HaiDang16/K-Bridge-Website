namespace K_Bridge.Models
{
    public enum NotificationType
    {
        PostApproval,
        NewUser,
        Periodic, 
        Global,
        PostUpdate
    }

    public class Notification : BaseModel
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; } = false;

        // Foreign key
        public int? UserID { get; set; }
        public User? User { get; set; }

        // Foreign key
        public int? PostID { get; set; }
        public Post? Post { get; set; }

        public NotificationType Type { get; set; }
    }
}
