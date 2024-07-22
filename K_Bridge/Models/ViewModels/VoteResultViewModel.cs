namespace K_Bridge.Models.ViewModels
{
    public class VoteResultViewModel
    {
        public int VoteOptionID { get; set; } // ID của tùy chọn
        public string Title { get; set; } // Tiêu đề của tùy chọn
        public int VoteCount { get; set; } // Số lượng phiếu bầu cho tùy chọn
    }
}
