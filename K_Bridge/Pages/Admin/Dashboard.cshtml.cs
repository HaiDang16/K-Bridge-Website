using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Bridge.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                // Chuyển hướng về trang đăng nhập nếu chưa đăng nhập
                return RedirectToPage("/Login");
            }
            // Xử lý logic hiển thị dashboard
            return Page();
        }
    }
}
