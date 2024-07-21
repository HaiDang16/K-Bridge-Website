namespace K_Bridge.Models
{
    public class Reply : BaseModel
    {
        public string? Content { get; set; }
        public string? Status { get; set; } //Disable - Enable

        // Foreign Key
        public int UserID { get; set; }
        public User? User { get; set; }
        public int PostID { get; set; }
        public Post Post { get; set; }

        public ICollection<Reply_Like>? Reply_Likes { get; set; }

    }
}
