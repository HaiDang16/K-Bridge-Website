namespace K_Bridge.Models
{
    public class Reply : BaseModel
    {
        public string? Content { get; set; }
        public string? Status { get; set; } //Disable - Enable
        public int UserID { get; set; }
        public int PostID { get; set; }
    }
}
