namespace K_Bridge.Models
{
    public class Topic : BaseModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? LinkIcon { get; set; }
        public string? Status { get; set; }

        // Foreign key
        public int ForumID { get; set; }
        public Forum? Forum { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
