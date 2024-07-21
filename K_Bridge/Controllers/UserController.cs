using Ganss.Xss;
using K_Bridge.Infrastructure;
using K_Bridge.Models;
using K_Bridge.Models.ViewModels;
using K_Bridge.Repositories;
using K_Bridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace K_Bridge.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private CodeGenerationService _codeGenerationService;
        private UserService _userService;


        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HtmlSanitizer _sanitizer;


        public UserController(
            IUserRepository userRepository,
            CodeGenerationService codeGenerationService,
            UserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _codeGenerationService = codeGenerationService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;

            _sanitizer = new HtmlSanitizer();
        }

        [Route("/UserProfile")]
        public IActionResult Index(int id)
        {
            var user = _userRepository.GetUserById(id);
            ViewBag.UserInfo = user;
            return View();
        }



        [Route("/UserProfile/EditDisplayName")]
        public IActionResult EditDisplayName(int id)
        {
            return View();
        }

        [HttpGet("/UserProfile/EditUserName")]
        public IActionResult EditUserName(int id)
        {
            ViewBag.UserID = id;
            return View();
        }

        [HttpPost("/UserProfile/EditUserName")]
        public IActionResult EditUserName(UpdateUsernameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }
            User? user = HttpContext.Session.GetJson<User>("user");

            if (user == null)
            {
                ModelState.AddModelError("", "Người dùng không tồn tại.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            if (model.NewUserName.Equals(user.Username))
            {
                ModelState.AddModelError("", "Tên đăng nhập trùng với tên đăng nhập hiện tại.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                ModelState.AddModelError("", "Mật khẩu không đúng.");
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            // Update the username
            user.Username = model.NewUserName;
            user.UpdatedAt = DateTime.Now;
            _userRepository.UpdateUserClient(user);

            // Redirect to profile page or another appropriate location
            return Json(new { success = true, userId = user.ID });
        }


        [HttpGet("/UserProfile/EditEmail")]
        public IActionResult EditEmail(int id)
        {
            ViewBag.UserID = id;
            return View();
        }

        [HttpPost("/UserProfile/EditEmail")]
        public IActionResult EditEmail(UpdateEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }
            User? user = HttpContext.Session.GetJson<User>("user");

            if (user == null)
            {
                ModelState.AddModelError("", "Người dùng không tồn tại.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            if (!model.Username.Equals(user.Username) || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            if (model.NewEmail.Equals(user.Email))
            {
                ModelState.AddModelError("", "Email trùng với email hiện tại.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }


            // Update the username
            user.Email = model.NewEmail;
            user.UpdatedAt = DateTime.Now;
            _userRepository.UpdateUserClient(user);

            // Redirect to profile page or another appropriate location
            return Json(new { success = true, userId = user.ID });
        }

        [HttpGet("/UserProfile/EditPhoneNum")]
        public IActionResult EditPhoneNum(int id)
        {
            ViewBag.UserID = id;

            return View();
        }

        [HttpPost("/UserProfile/EditPhoneNum")]
        public IActionResult EditPhoneNum(UpdatePhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }
            User? user = HttpContext.Session.GetJson<User>("user");

            if (user == null)
            {
                ModelState.AddModelError("", "Người dùng không tồn tại.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            if (!model.Username.Equals(user.Username) || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            if (model.NewPhoneNumber.Equals(user.PhoneNumber))
            {
                ModelState.AddModelError("", "Số điện thoại trùng với số điện thoại hiện tại.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            // Update the username
            user.PhoneNumber = model.NewPhoneNumber;
            user.UpdatedAt = DateTime.Now;
            _userRepository.UpdateUserClient(user);

            // Redirect to profile page or another appropriate location
            return Json(new { success = true, userId = user.ID });
        }

        [Route("/UserProfile/EditPass")]
        public IActionResult EditPass(int id)
        {
            return View();
        }

        [Route("/UserProfile/EditProfile")]
        public IActionResult EditProfile()
        {
            return View();
        }
    }
}