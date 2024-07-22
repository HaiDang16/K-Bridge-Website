namespace K_Bridge.Models
{
    public class VoteOption : BaseModel
    {
        public string? Title { get; set; }
        public int Quantity { get; set; }

        // Foreign key
        public int VoteID { get; set; }
        public Vote Vote { get; set; }

        public ICollection<UserVote>? UserVotes { get; set; }

    }
}
