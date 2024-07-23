using K_Bridge.Models;
using K_Bridge.Repositories;
using K_Bridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace K_Bridge.Pages.Admin.Manager
{
    public class EditModel : PageModel
    {
        private readonly IAdminAccountRepository _adminAccountRepository;

        public EditModel(IAdminAccountRepository adminAccountRepository)
        {
            _adminAccountRepository = adminAccountRepository;
        }
        [BindProperty]
        public string Code { get; set; }

        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public IFormFile Avatar { get; set; }
        public int ID { get; set; }

        public void OnGet(int id)
        {
            var admin = _adminAccountRepository.GetAdminAccountById(id);
            if (admin != null)
            {
                Code = admin.Code;
                Username = admin.Username;
                Email = admin.Email;
                ID = admin.ID;
            }
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }
            var existingAdmin = _adminAccountRepository.GetAdminAccountById(id);
            if (existingAdmin == null)
            {
                return new JsonResult(new { success = false, errors = new List<string> { "Tài khoản không tồn tại." } });
            }

            if (existingAdmin.Email != Email)
            {
                var checkEmail = _adminAccountRepository.Admin_Accounts.FirstOrDefault(u => u.Email == Email);
                if (checkEmail != null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        errors = new List<string> { "Email đã tồn tại." }
                    });
                }
            }

            if (existingAdmin.Username != Username)
            {
                var checkUsername = _adminAccountRepository.Admin_Accounts.FirstOrDefault(u => u.Username == Username);
                if (checkUsername != null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        errors = new List<string> { "Tên đăng nhập đã tồn tại." }
                    });
                }
            }


            string hashedPassword;
            if (!string.IsNullOrEmpty(Username))
            {
                hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
                existingAdmin.Password = hashedPassword;
            }

            existingAdmin.Username = Username;
            existingAdmin.Email = Email;

            // Lưu avatar mới
            if (Avatar != null && Avatar.Length > 0)
            {
                // Tạo tên file mới với dấu thời gian
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                var fileName = $"{timestamp}_{Path.GetFileName(Avatar.FileName)}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatars", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Avatar.CopyTo(stream);
                }
                existingAdmin.Avatar = fileName;
            }
            try
            {
                _adminAccountRepository.UpdateAdminAccount(existingAdmin);
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, errors = new List<string> { "Có lỗi xảy ra khi lưu tài khoản." } });
            }
        }
    }
}