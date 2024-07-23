using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models.ViewModels
{
    public class PostViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tựa bài viết")]
        //[StringLength(100)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung bài viết")]
        public string? Content { get; set; }

        public string? Question { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public bool IsUnlimited { get; set; }
        public string? CloseAfter { get; set; }
    }
}
