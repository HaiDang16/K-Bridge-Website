using K_Bridge.Models;
using K_Bridge.Repositories;
using K_Bridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Pages.Admin.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly CodeGenerationService _codeGenerationService;
        public readonly string NewCode;


        public CreateModel(IUserRepository userRepository, CodeGenerationService codeGenerationService)
        {
            _userRepository = userRepository;
            _codeGenerationService = codeGenerationService;
            NewCode = _codeGenerationService.GenerateNewCode<User>("USER");
        }

        [BindProperty]
        [Required]
        public string ReqUsername { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string ReqEmail { get; set; }

        [BindProperty]
        [Required]
        [MinLength(10)]
        public string ReqPhoneNumber { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string ReqPassword { get; set; }

        [BindProperty]
        public IFormFile ReqProfilePicture { get; set; }


        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var checkEmail = _userRepository.Users
                .FirstOrDefaultAsync(u => u.Email == ReqEmail);

            if (checkEmail.Result != null)
            {
                ModelState.AddModelError("ReqEmail", "Email đã được dùng để đăng ký");
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();
                return Page();
            }

            var checkUsername = _userRepository.Users
                .FirstOrDefaultAsync(u => u.Username == ReqUsername);

            if (checkUsername.Result != null)
            {
                ModelState.AddModelError("ReqUsername", "Tên đăng nhập đã tồn tại");
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();
                return Page();
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(ReqPassword);
            string newCode = _codeGenerationService.GenerateNewCode<User>("USER");


            var newUser = new User
            {
                Code = newCode,
                Username = ReqUsername,
                Email = ReqEmail,
                PhoneNumber = ReqPhoneNumber,
                Password = hashedPassword,
                Status = "Active"
            };
            if (ReqProfilePicture != null)
            {
                var fileName = Path.GetFileName(ReqProfilePicture.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ReqProfilePicture.CopyToAsync(stream);
                }

                newUser.Avatar = "/uploads/" + fileName;
            }

            _userRepository.SaveUser(newUser);

            return RedirectToPage("/Admin/Accounts/List");
        }
    }
}
