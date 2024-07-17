namespace K_Bridge.Models
{
    public class Post_Like : BaseModel
    {
        public bool IsLike { get; set; }
        public int PostID { get; set; }
        public Post? Post { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }

    }
}
