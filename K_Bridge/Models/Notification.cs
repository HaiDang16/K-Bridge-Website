namespace K_Bridge.Models
{
    public enum NotificationType
    {
        PostApproved, // Bài viết đã được duyệt
        NewUser, //Có tài khoản mới đăng ký
        Periodic,
        Global,
        PostUpdate,
        NewPost, //Có bài viết mới cần duyệt
        PostRejected, //Bài viết đã bị từ chối
        PostBlocked, //Bài viết đã bị khoá
        NewReply, //Bình luận mới
        NewLike, //Like mới
        NewDisLike, //Dislike mới
        NewLikeReply,
        NewDislikeReply,
        NewVote,
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

        public int? AdminID { get; set; }
        public Admin_Accounts? Admin { get; set; }
        public int? ReplyID { get; set; }
        public Reply? Reply { get; set; }

        public int? VoteID { get; set; }
        public Vote? Vote { get; set; }


        public NotificationType NotiType { get; set; }
    }
}
