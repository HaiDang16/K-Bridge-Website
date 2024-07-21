﻿using Ganss.Xss;
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
        private ILikeRepository _likeRepository;

        private CodeGenerationService _codeGenerationService;
        private UserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HtmlSanitizer _sanitizer;


        public PostController(IPostRepository postRepository,
            IReplyRepository replyRepository,
            ILikeRepository likeRepository,
            CodeGenerationService codeGenerationService,
            UserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _replyRepository = replyRepository;
            _likeRepository = likeRepository;

            _codeGenerationService = codeGenerationService;
            _userService = userService;
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
                return RedirectToAction("Index", "Home"); // Redirect to a page that lists all posts
            }
            return View(post);
        }

        [HttpGet("Details")]
        public IActionResult Details([FromQuery] int post, [FromQuery] string sort = "helpful")
        {
            //int? postId = EncryptIDHelper.DecryptID(post);
            if (post != 0)
            {
                // Increment view count
                _postRepository.IncrementViewCount(post);

                // Get detail of post
                var postDetails = _postRepository.GetPostByID(post);
                if (postDetails == null)
                    return NotFound("Post not found.");

                // Initialize status like/dislike post of user
                var userLikeStatus = 0; // 0: chưa like/dislike, 1: đã like, -1: đã dislike

                User? user = HttpContext.Session.GetJson<User>("user");

                if (user != null)
                {
                    // Get status like / dislike post of user
                    var existingLike = _likeRepository.GetExistPostLike(post, user.ID);
                    if (existingLike != null)
                        userLikeStatus = existingLike.IsLike ? 1 : -1;
                }

                // Get all reply of post
                var allRepliesWithLike = postDetails.Replies.Select(r => new ReplyViewModel
                {
                    Reply = r,
                    UserLikeStatus = user != null
                                    ? _likeRepository.GetUserReplyLikeStatus(r.ID, user.ID)
                                    : 0,
                    LikeCount = _likeRepository.GetReplyLikeCount(r.ID),
                    DislikeCount = _likeRepository.GetReplyDislikeCount(r.ID),
                    AllLikeCount = _likeRepository.GetReplyLikeCount(r.ID) - _likeRepository.GetReplyDislikeCount(r.ID)
                }).ToList();

                // Get total like/dislike of post
                var totalLikes = _likeRepository.GetPostLikeCount(post);
                var totalDislikes = _likeRepository.GetPostDislikeCount(post);

                ViewBag.Post = postDetails;
                ViewBag.Sort = sort;
                ViewBag.TotalLikesMinusDislikes = totalLikes - totalDislikes;
                ViewBag.UserLikeStatus = userLikeStatus;
                ViewBag.RepliesWithLike = allRepliesWithLike;

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
            var postDetails = _postRepository.GetPostByID(postId);

            // Initialize status like/dislike post of user
            var userLikeStatus = 0; // 0: chưa like/dislike, 1: đã like, -1: đã dislike

            User? user = HttpContext.Session.GetJson<User>("user");

            if (user != null)
            {
                // Get status like / dislike post of user
                var existingLike = _likeRepository.GetExistPostLike(postId, user.ID);
                if (existingLike != null)
                    userLikeStatus = existingLike.IsLike ? 1 : -1;
            }

            // Get all reply of post
            var allRepliesWithLike = postDetails.Replies.Select(r => new ReplyViewModel
            {
                Reply = r,
                UserLikeStatus = user != null
                                ? _likeRepository.GetUserReplyLikeStatus(r.ID, user.ID)
                                : 0,
                LikeCount = _likeRepository.GetReplyLikeCount(r.ID),
                DislikeCount = _likeRepository.GetReplyDislikeCount(r.ID),
                AllLikeCount = _likeRepository.GetReplyLikeCount(r.ID) - _likeRepository.GetReplyDislikeCount(r.ID)
            }).ToList();

            // Sort reply
            allRepliesWithLike = sort switch
            {
                "newest" => allRepliesWithLike.OrderByDescending(r => r.Reply.CreatedAt).ToList(),
                "oldest" => allRepliesWithLike.OrderBy(r => r.Reply.CreatedAt).ToList(),
                "helpful" => allRepliesWithLike.OrderByDescending(r => r.LikeCount).ThenByDescending(r => r.Reply.CreatedAt).ToList(),
                _ => allRepliesWithLike.OrderBy(r => r.Reply.CreatedAt).ToList()
            };

            return PartialView("_RepliesPartial", allRepliesWithLike);
        }

        [HttpPost("Like")]
        public IActionResult Like(int postId)
        {
            var currentUser = _userService.GetCurrentUser();
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to create a reply.";
                return RedirectToAction("Details", new { post = postId });
            }

            var existingLike = _likeRepository.GetExistPostLike(postId, currentUser.ID);

            if (existingLike != null)
            {
                if (existingLike.IsLike)
                    _likeRepository.DeleteExistPostLike(existingLike);
                else
                    _likeRepository.UpdatePostLike(existingLike, true);
            }
            else
            {
                var postLike = new Post_Like
                {
                    IsLike = true,
                    PostID = postId,
                    UserID = currentUser.ID,
                };

                _likeRepository.SavePostLike(postLike);
            }
            var likeCount = _likeRepository.GetPostLikeCount(postId);
            var dislikeCount = _likeRepository.GetPostDislikeCount(postId);

            return Json(new { likeCount, dislikeCount });
        }

        [HttpPost("Dislike")]
        public IActionResult Dislike(int postId)
        {
            var currentUser = _userService.GetCurrentUser();
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to create a reply.";
                return RedirectToAction("Details", new { post = postId });
            }

            var existingLike = _likeRepository.GetExistPostLike(postId, currentUser.ID);

            if (existingLike != null)
            {
                if (!existingLike.IsLike)
                    _likeRepository.DeleteExistPostLike(existingLike);
                else
                    _likeRepository.UpdatePostLike(existingLike, false);
            }
            else
            {
                var postLike = new Post_Like
                {
                    IsLike = false,
                    PostID = postId,
                    UserID = currentUser.ID,
                };

                _likeRepository.SavePostLike(postLike);
            }
            var likeCount = _likeRepository.GetPostLikeCount(postId);
            var dislikeCount = _likeRepository.GetPostDislikeCount(postId);

            return Json(new { likeCount, dislikeCount });
        }

        [HttpPost("ReplyLike")]
        public IActionResult ReplyLike(int replyId)
        {
            var currentUser = _userService.GetCurrentUser();
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to create a reply.";
                return RedirectToAction("Details", new { reply = replyId });
            }

            var existingLike = _likeRepository.GetExistReplyLike(replyId, currentUser.ID);

            if (existingLike != null)
            {
                if (existingLike.IsLike)
                    _likeRepository.DeleteExistReplyLike(existingLike);
                else
                    _likeRepository.UpdateReplyLike(existingLike, true);
            }
            else
            {
                var replyLike = new Reply_Like
                {
                    IsLike = true,
                    ReplyID = replyId,
                    UserID = currentUser.ID,
                };

                _likeRepository.SaveReplyLike(replyLike);
            }
            var likeCount = _likeRepository.GetReplyLikeCount(replyId);
            var dislikeCount = _likeRepository.GetReplyDislikeCount(replyId);

            int userLikeStatus = _likeRepository.ToggleReplyLike(replyId, currentUser.ID, true);


            return Json(new { likeCount, dislikeCount, userLikeStatus });
        }

        [HttpPost("ReplyDislike")]
        public IActionResult ReplyDislike(int replyId)
        {
            var currentUser = _userService.GetCurrentUser();
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to create a reply.";
                return RedirectToAction("Details", new { reply = replyId });
            }

            var existingLike = _likeRepository.GetExistReplyLike(replyId, currentUser.ID);

            if (existingLike != null)
            {
                if (!existingLike.IsLike)
                    _likeRepository.DeleteExistReplyLike(existingLike);
                else
                    _likeRepository.UpdateReplyLike(existingLike, true);
            }
            else
            {
                var replyLike = new Reply_Like
                {
                    IsLike = false,
                    ReplyID = replyId,
                    UserID = currentUser.ID,
                };

                _likeRepository.SaveReplyLike(replyLike);
            }
            var likeCount = _likeRepository.GetReplyLikeCount(replyId);
            var dislikeCount = _likeRepository.GetReplyDislikeCount(replyId);
            int userLikeStatus = _likeRepository.ToggleReplyLike(replyId, currentUser.ID, false);


            return Json(new { likeCount, dislikeCount, userLikeStatus });
        }

    }
}
