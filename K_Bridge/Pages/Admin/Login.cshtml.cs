using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using K_Bridge.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace K_Bridge.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly KBridgeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(KBridgeDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // Kiểm tra nếu người dùng đang yêu cầu đăng xuất
            if (Request.Query["action"] == "logout")
            {
                // Xóa tất cả session
                _httpContextAccessor.HttpContext.Session.Clear();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                var admin = await _context.Admin_Accounts.FirstOrDefaultAsync(u => u.Username == Username && u.Password == Password);

                if (admin != null)
                {
                    // Lưu thông tin phiên
                    HttpContext.Session.SetInt32("AdminID", admin.ID);
                    HttpContext.Session.SetString("AdminUsername", admin.Username);
                    HttpContext.Session.SetString("AdminRole", admin.Role);
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
