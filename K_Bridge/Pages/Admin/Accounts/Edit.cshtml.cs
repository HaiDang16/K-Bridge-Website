using K_Bridge.Models;
using K_Bridge.Repositories;
using K_Bridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Pages.Admin.Accounts
{
    public class EditModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public EditModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public string Code { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public IFormFile ProfilePicture { get; set; }

        public void OnGet(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
            }

            Code = user.Code;
            Username = user.Username;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
        }


        public IActionResult OnPost(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            // Update only the fields that are provided
            if (!string.IsNullOrEmpty(Username))
            {
                user.Username = Username;
            }

            if (!string.IsNullOrEmpty(Email))
            {
                user.Email = Email;
            }

            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                user.PhoneNumber = PhoneNumber;
            }

            if (!string.IsNullOrEmpty(Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(Password);
            }

            if (ProfilePicture != null)
            {
                var fileName = Path.GetFileName(ProfilePicture.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ProfilePicture.CopyToAsync(stream);
                }

                user.Avatar = "/uploads/" + fileName;
            }

            _userRepository.UpdateUser(user);

            return RedirectToPage("/Admin/Accounts/List");
        }
    }
}
