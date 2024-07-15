using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models
{
    public class User : BaseModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Biography { get; set; }
        public int Reputation { get; set; } = 0;
        public int PostCount { get; set; } = 0;
        public string? Status { get; set; }

        // Navigation property for related posts
        public ICollection<Post>? Posts { get; set; }
    }
}