namespace K_Bridge.Models
{
    public class Vote : BaseModel
    {
        public string? Question { get; set; }
        public int OptionCount { get; set; }
        public bool IsUnlimited { get; set; } = false;
        public string? CloseAfter { get; set; } //hour - day - week - month
        public string? Status { get; set; } // Open - Close

        // Foreign key
        public int PostID { get; set; }
        public Post Post { get; set; }

        public ICollection<VoteOption>? VoteOptions { get; set; }
        public ICollection<Notification>? Notifications { get; set; }

        public DateTime GetCloseTime()
        {
            if (string.IsNullOrEmpty(CloseAfter))
            {
                return DateTime.MaxValue; // No close time specified
            }

            var closeTime = DateTime.UtcNow;

            switch (CloseAfter.ToLower())
            {
                case "hour":
                    closeTime = CreatedAt.AddHours(1);
                    break;
                case "day":
                    closeTime = CreatedAt.AddDays(1);
                    break;
                case "week":
                    closeTime = CreatedAt.AddDays(7);
                    break;
                case "month":
                    closeTime = CreatedAt.AddMonths(1);
                    break;
                default:
                    closeTime = DateTime.MaxValue;
                    break;
            }

            return closeTime;
        }
    }
}
