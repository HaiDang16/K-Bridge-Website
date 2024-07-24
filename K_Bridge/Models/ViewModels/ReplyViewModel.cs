namespace K_Bridge.Models.ViewModels
{
    public class ReplyViewModel
    {
        public Reply? Reply { get; set; }
        public int UserLikeStatus { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int AllLikeCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

    }
}
