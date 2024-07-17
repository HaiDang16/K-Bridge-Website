using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using K_Bridge.Models;
using Microsoft.EntityFrameworkCore.Migrations;
namespace K_Bridge.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly KBridgeDbContext _context;

        public LoginModel(KBridgeDbContext dbContext)
        {
            _context = dbContext;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

                if (user != null)
                {
                    // Lưu thông tin phiên
                    HttpContext.Session.SetInt32("AdminAccountID", user.ID);
                    return RedirectToPage("/Admin/Dashboard");
                }
                else
                {
                    ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }
            }
            else
            {
                ErrorMessage = "Có gì đó không đúng, hãy thử lại.";
            }
            return Page();
        }
    }
}
