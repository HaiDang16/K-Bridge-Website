namespace K_Bridge.Models.ViewModels
{
    public class EditPostViewModel
    {
        // Properties related to Post
        public int PostID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageLink { get; set; }
        public string? Status { get; set; } // Status like Pending, Accepted, Rejected, Blocked
        public bool IsVote { get; set; }

        // Properties related to Vote
        public string? Question { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public bool IsUnlimited { get; set; }
        public string? CloseAfter { get; set; } // e.g., hour, day, week, month

        // Additional properties for display or form management
        public string? ExistingVoteOptions { get; set; } // If needed to display existing options
    }
}
