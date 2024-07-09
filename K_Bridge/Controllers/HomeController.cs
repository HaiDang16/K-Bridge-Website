using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using K_Bridge.Models;
using Microsoft.AspNetCore.Identity;
using K_Bridge.Services;
using K_Bridge.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using K_Bridge.Infrastructure;

namespace K_Bridge.Controllers;

public class HomeController : Controller
{

    private IUserRepository _repository;
    private CodeGenerationService _codeGenerationService;

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public HomeController(IUserRepository repository, CodeGenerationService codeGenerationService)
    {
        _repository = repository;
        _codeGenerationService = codeGenerationService;
    }

    public IActionResult Index()
    {
        User? user = HttpContext.Session.GetJson<User>("user");
        bool isLoggedIn = (user != null);
        ViewBag.IsLoggedIn = isLoggedIn;
        return View();
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

            if (existingUser.Result != null)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại");
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();
                return Json(new { success = false, errors = errors });
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

            HttpContext.Session.SetJson("user", newUser);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToList();
            return Json(new { success = false, errors = errors });
        }
    }
}
