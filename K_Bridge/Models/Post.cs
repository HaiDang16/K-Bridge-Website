namespace K_Bridge.Models
{
    public class Post : BaseModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public int ViewCount { get; set; }
        public string? ImageLink { get; set; }
        public string? Status { get; set; } //Disable - Enable

        // Foreign key
        public int UserID { get; set; }
        public User? User { get; set; } // Navigation property

        public int TopicID { get; set; }
        public Topic? Topic { get; set; }

    }
}



