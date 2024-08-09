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
using Ganss.Xss;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace K_Bridge.Controllers;

public class HomeController : Controller
{

    private IUserRepository _userRepository;
    private IPostRepository _postRepository;
    private IForumRepository _forumRepository;
    private IKBridgeRepository _kBridgeRepository;
    private ITopicRepository _topicRepository;
    private IGlobalChatRepository _chatRepository;

    private readonly HtmlSanitizer _sanitizer;


    private CodeGenerationService _codeGenerationService;

    private readonly IHttpContextAccessor _httpContextAccessor;



    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public HomeController(IUserRepository userRepository, IPostRepository postRepository, IForumRepository forumRepository,
        IKBridgeRepository kBridgeRepository, ITopicRepository topicRepository,
        IGlobalChatRepository chatRepository,
        CodeGenerationService codeGenerationService, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _codeGenerationService = codeGenerationService;
        _httpContextAccessor = httpContextAccessor;
        _postRepository = postRepository;
        _forumRepository = forumRepository;
        _kBridgeRepository = kBridgeRepository;
        _topicRepository = topicRepository;
        _chatRepository = chatRepository;
        _sanitizer = new HtmlSanitizer();

    }

    public IActionResult Index()
    {
        // Bài viết mới nhất
        var latestPost = _postRepository.GetLatestPost();
        ViewBag.LatestPost = latestPost;

        // Forum with topics
        var lstForum = _forumRepository.GetForumsWithTopicsAndLatestPosts();
        ViewBag.Forum = lstForum;

        User? user = HttpContext.Session.GetJson<User>("user");
        ViewBag.UserLastLogin = user?.LastLogin;

        return View();
    }

    [HttpPost]
    public IActionResult Register(LoginRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var checkEmail = _userRepository.Users
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

            var checkUsername = _userRepository.Users
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
            _userRepository.SaveUser(newUser);

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
            var user = _userRepository.Users.FirstOrDefaultAsync(u => u.Email == model.LoginModel.User || u.Username == model.LoginModel.User).Result;

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.LoginModel.Password, user.Password))
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác");
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            // Update LastLogin property
            user.LastLogin = DateTime.UtcNow;

            _userRepository.UpdateUser(user);

            HttpContext.Session.SetJson("user", user);

            return Json(new { success = true, user = new { username = user.Username, email = user.Email } });
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors = errors });
        }
    }

    [HttpPost]
    public IActionResult SendMessage([FromBody] GlobalChat message)
    {
        if (ModelState.IsValid)
        {
            message.SendAt = DateTime.Now; // Set message timestamp
            _chatRepository.AddMessage(message); // Add message to repository

            return Ok(); // Return 200 OK status
        }
        return BadRequest();
    }

    [Route("Search")]
    [HttpGet("Search")]
    public IActionResult Searching([FromQuery] string key)
    {
        var searchResults = _postRepository.Posts

        .Include(p => p.User) // Include user details of the post
        .Include(p => p.Topic) // Include topic details of the post
            .ThenInclude(t => t.Forum) // Include forum details of the topic
        .Include(p => p.Replies) // Include replies of the post
            .ThenInclude(r => r.User) // Include user details of each reply
         .Where(p => (p.Title.Contains(key) || p.Content.Contains(key)) && p.Status == "Approved")
        .ToList();

        ViewBag.SearchKey = key;
        ViewBag.SearchResults = searchResults;
        return View();
    }


    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        // Clear the user's session
        HttpContext.Session.Remove("user");

        // Optionally, you can clear the entire session
        HttpContext.Session.Clear();

        // Redirect to the login page or home page
        return RedirectToAction("Index", "Home"); // or RedirectToAction("Login", "Account");
    }

    [HttpGet("/Topic/List")]
    public IActionResult TopicList(int forum)
    {
        var forumWithTopic = _forumRepository.GetTopicWithForumById(forum);
        ViewBag.ForumName = forumWithTopic.Name;

        ViewBag.Topics = forumWithTopic.Topics;

        return View();
    }

    [HttpGet("/Topic/Post/List")]
    public IActionResult PostList(int topic, int page = 1, int pageSize = 3)
    {
        var postList = _postRepository.GetPostsByTopicFilter(topic);

        ViewBag.Posts = postList.ToList();
        ViewBag.TopicID = _topicRepository.GetTopicById(topic).ID;
        ViewBag.TopicName = _topicRepository.GetTopicById(topic).Name;
        return View();
    }
    [HttpGet("/Topic/Post/PostListPartial")]
    public IActionResult PostListPartial(int topic, bool noAnswer, bool noView, string sortOrder, string timeOrder)
    {
        var postList = _postRepository.GetPostsByTopicFilter(topic);

        // Apply filters
        if (noAnswer)
        {
            postList = postList.Where(p => !p.Replies.Any());
        }

        if (noView)
        {
            postList = postList.Where(p => p.ViewCount == 0);
        }

        // Apply sorting
        switch (sortOrder)
        {
            case "trending":
                var sevenDaysAgo = DateTime.Now.AddDays(-7);
                postList = _postRepository.PostsFilterTrending(postList);
                break;

            case "mostHelpful":
                postList = _postRepository.PostsFilterHelpful(postList);
                break;
            default:
                postList = postList.OrderByDescending(p => p.CreatedAt);
                break;
        }

        // Apply time order
        if (timeOrder == "ascending")
        {
            postList = postList.OrderBy(p => p.CreatedAt);
        }
        else
        {
            postList = postList.OrderByDescending(p => p.CreatedAt);
        }

        ViewBag.Posts = postList.ToList();
        ViewBag.TopicID = _topicRepository.GetTopicById(topic).ID;
        ViewBag.TopicName = _topicRepository.GetTopicById(topic).Name;
        return PartialView("_PostListPartial");
    }

    [HttpGet("/Newest")]
    public IActionResult Newest()
    {
        // Bài viết mới nhất
        var latestPost = _postRepository.GetLatestPost();
        ViewBag.LatestPost = latestPost;
        return View();
    }
}
