using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel(string? user, string? password)
        {
            User = user;
            Password = password;
        }

        [Required(ErrorMessage = "Vui lòng nhập email hoặc tên đăng nhập")]
        public string? User { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
