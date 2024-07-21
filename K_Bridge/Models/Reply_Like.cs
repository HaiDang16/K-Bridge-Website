namespace K_Bridge.Models
{
    public class Reply_Like : BaseModel
    {
        public bool IsLike { get; set; }

        public int ReplyID { get; set; }
        public Reply? Reply { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }

    }
}
