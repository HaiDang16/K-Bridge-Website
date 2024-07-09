using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel(string? username, string? email, string? password, string? confirmPassword)
        {
            Username = username;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage ="Định dạng email không đúng")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập xác thực mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string? ConfirmPassword { get; set; }

/*        //Login
        [Required(ErrorMessage = "Vui lòng nhập email hoặc tên đăng nhập")]
        public string? LoginUser { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string? LoginPassword { get; set; }
*/

    }
}
