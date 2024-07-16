using Ganss.Xss;
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

        private readonly HtmlSanitizer _sanitizer;


        public PostController(IPostRepository postRepository,
            IReplyRepository replyRepository,
            CodeGenerationService codeGenerationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _replyRepository = replyRepository;

            _codeGenerationService = codeGenerationService;
            _httpContextAccessor = httpContextAccessor;

            _sanitizer = new HtmlSanitizer();
/*            _sanitizer.AllowedTags.Clear();
            _sanitizer.AllowedTags.Add("b");
            _sanitizer.AllowedTags.Add("i");
            _sanitizer.AllowedTags.Add("u");
            _sanitizer.AllowedTags.Add("ol");
            _sanitizer.AllowedTags.Add("ul");
            _sanitizer.AllowedTags.Add("li");*/
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
        public IActionResult Details([FromQuery] int post, [FromQuery] string sort = "most_helpful")
        {
            //int? postId = EncryptIDHelper.DecryptID(post);
            if (post != 0)
            {
                var postDetails = _postRepository.GetPostByID(post);
                if (postDetails == null)
                    return NotFound("Post not found.");

                var allReplies = _replyRepository.GetRepliesByPostId(post);

                allReplies = sort switch
                {
                    "newest" => allReplies.OrderByDescending(r => r.CreatedAt).ToList(),
                    _ => allReplies.OrderBy(r => r.CreatedAt).ToList()
                    //_ => allReplies.OrderByDescending(r => r.IsHelpful).ThenByDescending(r => r.CreatedAt).ToList()
                };

                ViewBag.Post = postDetails;
                ViewBag.Reply = allReplies;
                ViewBag.Sort = sort;
                return View();
            }
           
            else
                return BadRequest("Invalid Post ID");
        }

        [HttpPost]
        public IActionResult SubmitReply(int postId, string answerContent)
        {
            var sanitizedContent = _sanitizer.Sanitize(answerContent);
            if (string.IsNullOrWhiteSpace(answerContent))
            {
                return BadRequest("Answer content cannot be empty.");
            }

            User? user = HttpContext.Session.GetJson<User>("user");

            if (user == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to create a reply.";
                return RedirectToAction("Details", new { post = postId });
            }


            var reply = new Reply
            {
                PostID = postId,
                Content = sanitizedContent,
                Status = "Enable",
                UserID = user.ID,
            };

            _replyRepository.SaveReply(reply);

            return RedirectToAction("Details", new { post = postId });
        }

        [HttpGet("GetReplies")]
        public IActionResult GetReplies(int postId, string sort = "newest")
        {
            var post = _postRepository.GetPostByID(postId);
            if (post == null)
                return NotFound("Post not found.");

            var allReplies = _replyRepository.GetRepliesByPostId(postId);

            allReplies = sort switch
            {
                "newest" => allReplies.OrderByDescending(r => r.CreatedAt).ToList(),
                _ => allReplies.OrderBy(r => r.CreatedAt).ToList()
                //_ => allReplies.OrderByDescending(r => r.IsHelpful).ThenByDescending(r => r.CreatedAt).ToList()
            };

            return PartialView("_RepliesPartial", allReplies);
        }

    }
}
