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
        }

        [BindProperty]
        public Admin_Accounts AdminAccount { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == AdminAccount.Username && u.Password == AdminAccount.Password);

                if (user != null)
                {
                    // Đăng nhập thành công
                    return RedirectToPage("/Admin/Dashboard");
                }
                else
                {
                    // Đăng nhập thất bại
                    ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    return Page();
                }
            }

            return Page();
        }
    }
}
