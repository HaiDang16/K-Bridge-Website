using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace K_Bridge.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var admin = await _adminRepository.AuthenticateAdmin(username, password);
            if (admin != null)
            {
                HttpContext.Session.SetString("AdminId", admin.ID.ToString());
                HttpContext.Session.SetString("AdminUsername", admin.Username);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        // Thêm các action khác nếu cần
    }
}
