namespace K_Bridge.Models.ViewModels
{
    public class LikeDislikeViewModel
    {
        public int PostId { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public bool? CurrentLikeStatus { get; set; }
    }
}
