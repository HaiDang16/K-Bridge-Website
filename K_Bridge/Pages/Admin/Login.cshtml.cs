using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using K_Bridge.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace K_Bridge.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly KBridgeDbContext _context;

        public LoginModel(KBridgeDbContext context)
        {
            _context = context;
            AdminAccount = new Admin_Accounts();
            ErrorMessage = "";
        }

        [BindProperty]
        public Admin_Accounts AdminAccount { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost method is being called.");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is valid.");

                try
                {
                    var user = _context.Admin_Accounts
                        .FirstOrDefault(u =>
                            u.Username.Equals(AdminAccount.Username, StringComparison.OrdinalIgnoreCase) &&
                            u.Password == AdminAccount.Password); // Consider using a hashed password comparison

                    if (user != null)
                    {
                        Console.WriteLine("User is found and authenticated.");

                        // Đăng nhập thành công
                        // Lưu thông tin người dùng vào session
                        HttpContext.Session.SetString("UserName", AdminAccount.Username);
                        HttpContext.Session.SetString("Avatar", AdminAccount.Avatar);
                        HttpContext.Session.SetString("Role", AdminAccount.Role);

                        Console.WriteLine("User information saved to session.");

                        return RedirectToPage("/Admin/Dashboard");
                    }
                    else
                    {
                        Console.WriteLine("User not found or authentication failed.");

                        // Đăng nhập thất bại
                        ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred during login: " + ex.Message);

                    // Ghi log lỗi
                    ErrorMessage = "Đã xảy ra lỗi trong quá trình đăng nhập. Vui lòng thử lại sau.";
                    // Log the exception: _logger.LogError(ex, "Error during login");
                }
            }
            else
            {
                Console.WriteLine("ModelState is invalid.");
            }

            return RedirectToPage("/Admin/Dashboard");
        }
    }
}
