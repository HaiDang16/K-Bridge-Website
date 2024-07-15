using K_Bridge.Infrastructure;
using K_Bridge.Models;
using K_Bridge.Models.ViewModels;
using K_Bridge.Repositories;
using K_Bridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace K_Bridge.Controllers
{
    [Route("[controller]")]
    public class PostController : Controller
    {
        private IPostRepository _repository;
        private CodeGenerationService _codeGenerationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostController(IPostRepository repository, CodeGenerationService codeGenerationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _codeGenerationService = codeGenerationService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("Create")]
        public IActionResult Create(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                User? user = HttpContext.Session.GetJson<User>("user");

                if (user == null)
                {
                    TempData["ErrorMessage"] = "You must be logged in to create a post.";
                    return RedirectToAction(nameof(Create));
                }

                string newCode = _codeGenerationService.GenerateNewCode<Post>("POST");
                var newPost = new Post
                {
                    Code = newCode,
                    Content = post.Content,
                    Title = post.Title,
                    Status = "Enable",
                    UserID = user.ID,
                };
                _repository.SavePost(newPost);
                return RedirectToAction(nameof(Create)); // Redirect to a page that lists all posts
            }
            return View(post);
        }

    }
}
