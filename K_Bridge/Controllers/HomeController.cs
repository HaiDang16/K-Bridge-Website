using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using K_Bridge.Models;
using Microsoft.AspNetCore.Identity;
using K_Bridge.Services;
using K_Bridge.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using K_Bridge.Infrastructure;
using K_Bridge.Pages.Admin;
using K_Bridge.Repositories;

namespace K_Bridge.Controllers;

public class HomeController : Controller
{

    private IUserRepository _repository;
    private CodeGenerationService _codeGenerationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public HomeController(IUserRepository repository, CodeGenerationService codeGenerationService, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _codeGenerationService = codeGenerationService;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(LoginRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var checkEmail = _repository.Users
                .FirstOrDefaultAsync(u => u.Email == model.RegisterModel.Email);

            if (model.RegisterModel.Password != model.RegisterModel.ConfirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu không khớp");
            }

            if (checkEmail.Result != null)
            {
                ModelState.AddModelError("Email", "Email đã được dùng để đăng ký");
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();
                return Json(new { success = false, errors = errors });
            }

            var checkUsername = _repository.Users
                .FirstOrDefaultAsync(u => u.Username == model.RegisterModel.Username);

            if (checkUsername.Result != null)
            {
                ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại");
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();
                return Json(new { success = false, errors = errors });
            }

            string newCode = _codeGenerationService.GenerateNewCode<User>("USER");
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.RegisterModel.Password);

            // Tạo người dùng mới
            var newUser = new User
            {
                Code = newCode,
                Username = model.RegisterModel.Username,
                Email = model.RegisterModel.Email,
                Password = hashedPassword,
                Status = "Active"
            };
            _repository.SaveUser(newUser);

            //HttpContext.Session.SetJson("user", newUser);

            return Json(new { success = true, message = "Đăng ký thành công!" });
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToList();
            return Json(new { success = false, errors = errors });
        }
    }

    [HttpPost]
    public IActionResult Login(LoginRegisterViewModel model)
    {

        if (ModelState.IsValid)
        {
            var user = _repository.Users.FirstOrDefaultAsync(u => u.Email == model.LoginModel.User || u.Username == model.LoginModel.User).Result;

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.LoginModel.Password, user.Password))
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác");
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            HttpContext.Session.SetJson("user", user);

            return Json(new { success = true, user = new { username = user.Username, email = user.Email } });
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors = errors });
        }
    }
    [Route("/Search/{SearchKey}")]
    public IActionResult Search(string SearchKey)
    {
        return View();
    }

    [Route("/ForgetPassword")]
    public IActionResult ForgetPassword()
    {
        return View();
    }
}
