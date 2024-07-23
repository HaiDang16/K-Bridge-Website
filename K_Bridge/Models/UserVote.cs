namespace K_Bridge.Models
{
    public class UserVote : BaseModel
    {
        public int UserID { get; set; }
        public User? User { get; set; }

        public int VoteOptionID { get; set; }
        public VoteOption? VoteOption { get; set; }

    }
}
