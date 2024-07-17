namespace K_Bridge.Models.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; } = new Post();
        public User User { get; set; } = new User();
        public Topic Topic { get; set; } = new Topic();
        public Forum Forum { get; set; } = new Forum();
    }
}
