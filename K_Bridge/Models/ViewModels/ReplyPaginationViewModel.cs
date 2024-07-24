namespace K_Bridge.Models.ViewModels
{
    public class ReplyPaginationViewModel
    {
        public List<ReplyViewModel> Replies { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PostId { get; set; }
        public string Sort { get; set; }
    }
}
