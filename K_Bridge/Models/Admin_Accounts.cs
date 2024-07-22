namespace K_Bridge.Models
{
    public class Admin_Accounts : BaseModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public DateTime LastLogin { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public ICollection<Notification>? Notifications { get; set; }
    }
}
