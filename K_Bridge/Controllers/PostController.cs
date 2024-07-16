using K_Bridge.Helpers;
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
        private IPostRepository _postRepository;
        private IReplyRepository _replyRepository;

        private CodeGenerationService _codeGenerationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostController(IPostRepository postRepository,
            IReplyRepository replyRepository,
            CodeGenerationService codeGenerationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _replyRepository = replyRepository;

            _codeGenerationService = codeGenerationService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Create")]
        public IActionResult Create([FromQuery] int Topic)
        {
            ViewBag.TopicID = Topic;
            return View();
        }
        [HttpPost("Create")]
        public IActionResult Create(PostViewModel post, int topicID)
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
                    TopicID = topicID
                };
                _postRepository.SavePost(newPost);
                return RedirectToAction(nameof(Create)); // Redirect to a page that lists all posts
            }
            return View(post);
        }

        [HttpGet("Details")]
        public IActionResult Details([FromQuery] int post)
        {
            //int? postId = EncryptIDHelper.DecryptID(post);
            if (post != 0)
            {
                var postDetails = _postRepository.GetPostByID(post);
                if (postDetails == null)
                    return NotFound("Post not found.");

                var allReply = _replyRepository.GetRepliesByPostId(post);

                ViewBag.Post = postDetails;
                ViewBag.Reply = allReply;

                return View();
            }
            else
            {
                return BadRequest("Invalid Post ID");
            }
        }

        [HttpPost]
        public IActionResult SubmitReply(int postId, string answerContent)
        {
            if (string.IsNullOrWhiteSpace(answerContent))
            {
                return BadRequest("Answer content cannot be empty.");
            }

            User? user = HttpContext.Session.GetJson<User>("user");

            if (user == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to create a reply.";
                return RedirectToAction(nameof(Details));
            }


            var reply = new Reply
            {
                PostID = postId,
                Content = answerContent,
                Status = "Enable",
                UserID = user.ID,
            };

            _replyRepository.SaveReply(reply);

            return RedirectToAction("Details", new { post = postId });
        }

    }
}
