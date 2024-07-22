namespace K_Bridge.Models
{
    public class Vote : BaseModel
    {
        public string? Question { get; set; }
        public int OptionCount { get; set; }
        public bool IsUnlimited { get; set; } = false;
        public string? CloseAfter { get; set; } //hour - day - week - month

        // Foreign key
        public int PostID { get; set; }
        public Post Post { get; set; }

        public ICollection<VoteOption> VoteOptions { get; set; }
    }
}
