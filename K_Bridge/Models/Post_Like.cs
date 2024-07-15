namespace K_Bridge.Models
{
    public class Post_Like : BaseModel
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
    }
}
