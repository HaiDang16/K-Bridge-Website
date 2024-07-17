using Microsoft.AspNetCore.Mvc;
using K_Bridge.Attributes;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Bridge.Pages.Admin
{
    [AdminAuth]
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
