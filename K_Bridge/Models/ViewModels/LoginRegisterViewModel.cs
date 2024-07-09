namespace K_Bridge.Models.ViewModels
{
    public class LoginRegisterViewModel
    {
        public LoginViewModel LoginModel { get; set; } = new LoginViewModel("username", "password");
        public RegisterViewModel RegisterModel { get; set; } = new RegisterViewModel("username", "email@gmail.com", "password", "password");
    }
}
