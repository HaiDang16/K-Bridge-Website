using K_Bridge.Models;
using K_Bridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using BCrypt.Net;
using K_Bridge.Models.ViewModels;

namespace K_Bridge.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _repository;
        private CodeGenerationService _codeGenerationService;

        public UserController(IUserRepository repository, CodeGenerationService codeGenerationService)
        {
            _repository = repository;
            _codeGenerationService = codeGenerationService;
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại chưa
                var existingUser = _repository.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (model.Password != confirmPassword)
                {
                    ModelState.AddModelError("", "Mật khẩu không khớp");

                }

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại");
                    return View("Index",model);
                }

                string newCode = _codeGenerationService.GenerateNewCode<User>("User");
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                // Tạo người dùng mới
                var newUser = new User
                {
                    Code = newCode,
                    Username = model.Username,
                    Email = model.Email,
                    Password = hashedPassword,
                    Status = "Active"
                };
                _repository.SaveUser(newUser);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}
