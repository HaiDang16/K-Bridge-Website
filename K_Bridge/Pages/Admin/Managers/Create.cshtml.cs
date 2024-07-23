using K_Bridge.Models;
using K_Bridge.Repositories;
using K_Bridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace K_Bridge.Pages.Admin.Manager
{
    public class CreateModel : PageModel
    {
        private readonly IAdminAccountRepository _adminAccountRepository;
        private readonly CodeGenerationService _codeGenerationService;

        public CreateModel(IAdminAccountRepository adminAccountRepository, CodeGenerationService codeGenerationService)
        {
            _adminAccountRepository = adminAccountRepository;
            _codeGenerationService = codeGenerationService;
            NewCode = _codeGenerationService.GenerateNewCode<Admin_Accounts>("ADMIN");
        }
        public string NewCode { get; set; }

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

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var checkEmail = _adminAccountRepository.Admin_Accounts
                .FirstOrDefault(u => u.Email == Email);

            if (checkEmail != null)
            {
                return new JsonResult(new
                {
                    success = false,
                    errors = new List<string> { "Email đã tồn tại." }
                });
            }
            var checkUsername = _adminAccountRepository.Admin_Accounts
               .FirstOrDefault(u => u.Username == Username);

            if (checkUsername != null)
            {
                return new JsonResult(new
                {
                    success = false,
                    errors = new List<string> { "Tên đăng nhập đã tồn tại." }
                });
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            string newCode = _codeGenerationService.GenerateNewCode<Admin_Accounts>("ADMIN");


            var newAdmin = new Admin_Accounts
            {
                Code = newCode,
                Username = Username,
                Email = Email,
                Password = hashedPassword,
                Status = "Active",
                Role = "Emp"
            };

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
                newAdmin.Avatar = fileName;
            }

            try
            {
                _adminAccountRepository.SaveAdminAccount(newAdmin);
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, errors = new List<string> { "Có lỗi xảy ra khi lưu chủ đề." } });
            }
        }
    }
}
