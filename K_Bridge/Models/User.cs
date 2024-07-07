namespace K_Bridge.Models
{
    public class User
    {
        public long? ID { get; set; }
        public string Username { get; set; } = string.Empty; //tên đăng nhập
        //public string DisplayName { get; set; } = string.Empty; //tên hiển thị
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        //public string BackgroundAvatar { get; set; } = "#D9D9D9"; //ảnh nền
        public DateTime LastLogin { get; set; } 
        public string Biography { get; set; } = string.Empty; //Giới thiệu bản thân
        public int Reputation { get; set; } //Điểm uy tín
        // public string Roles { get; set; } = string.Empty;
        public int PostCount { get; set; } = 0;//số lượng bài viết 
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}