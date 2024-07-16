namespace K_Bridge.Models
{
    public class Reply : BaseModel
    {
        public string? Content { get; set; }
        public string? Status { get; set; } //Disable - Enable

        // Foreign Key
        public int UserID { get; set; }
        public User User { get; set; } = new User();
        public int PostID { get; set; }
        public Post Post { get; set; } = new Post();
    }
}
