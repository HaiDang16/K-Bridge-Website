using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Bridge.Pages.Admin
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Xóa session
            HttpContext.Session.Clear();

            /*           // Xóa cookie xác thực (nếu có)
                       Response.Cookies.Delete("AuthCookie");*/

            // Chuyển hướng về trang đăng nhập
            return RedirectToPage("/Admin/Login");
        }
    }
}
