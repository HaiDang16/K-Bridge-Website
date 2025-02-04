﻿namespace K_Bridge.Models
{
    public class Post : BaseModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int ViewCount { get; set; }
        public string? ImageLink { get; set; }
        public string? Status { get; set; } //Pending - Accepted - Rejected - Blocked
        public bool IsVote { get; set; } = false;

        // Foreign key
        public int UserID { get; set; }
        public User? User { get; set; }

        public int TopicID { get; set; }
        public Topic? Topic { get; set; }

        public int VoteID { get; set; }
        public Vote? Vote { get; set; }

        public ICollection<Reply> Replies { get; set; }
        public ICollection<Post_Like>? Post_Likes { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
    }
}