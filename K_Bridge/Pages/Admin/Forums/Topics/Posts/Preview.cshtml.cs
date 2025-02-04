﻿using K_Bridge.Helpers;
using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Bridge.Pages.Admin.Forums.Topics.Posts
{
    public class PreviewModel : PageModel
    {
        private readonly IPostRepository _postRepository;
        private readonly IKBridgeRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IVoteRepository _voteRepository;


        public PreviewModel(IPostRepository postRepository, IKBridgeRepository repository,
            IUserRepository userRepository, ITopicRepository topicRepository, IVoteRepository voteRepository,
            INotificationRepository notificationRepository)
        {
            _postRepository = postRepository;
            _repository = repository;
            _userRepository = userRepository;
            _topicRepository = topicRepository;
            _notificationRepository = notificationRepository;
            _voteRepository = voteRepository;
        }
        
        public Post Post { get; set; }
        public int TotalPost { get; set; } = 0;
        public int DayJoined { get; set; }
        public string Status { get; set; } = "";
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public int ForumID { get; set; }
        public string ForumName { get; set; }


        public bool IsVote { get; set; }
        public Vote Vote { get; set; }
        public int[] VoteCountArr { get; set; }
        public List<VoteOption> VoteOptionsList { get; set; }

        public void OnGet(int id, int userId)
        {
            var postDetails = _postRepository.GetPostByID(id);

            var user = _userRepository.GetUserWithPostById(userId);
            var totalPost = user.Posts.Count;
            var daysJoined = (DateTime.UtcNow - user.CreatedAt).Days;

            Post = postDetails;

            if (Post == null)
            {
                Response.StatusCode = 404; // Set the status code to 404
                return; // Stop further processing
            }

            TotalPost = totalPost;
            DayJoined = daysJoined;
            Status = postDetails.Status;

            TopicID = postDetails.Topic.ID;
            TopicName = postDetails.Topic.Name ?? "Diễn đàn";
            ForumID = postDetails.Topic.ForumID;
            ForumName = postDetails.Topic.Forum.Name;

            if (Post.IsVote)
            {
                var voteDetails = _voteRepository.GetVoteById(Post.VoteID);
                IsVote = true;
                Vote = voteDetails;
                VoteCountArr = voteDetails.VoteOptions.Select(o => o.Quantity).ToArray();
                VoteOptionsList = voteDetails.VoteOptions.ToList();
            }
            else
            {
                IsVote = false;
            }
        }

        public IActionResult OnPost(int id, string status)
        {
            var post = _postRepository.GetPostByID(id);

            if (post == null)
            {
                return NotFound();
            }

            // Update post status based on the provided status
            post.Status = status switch
            {
                "approved" => "Approved",
                "rejected" => "Rejected",
                "blocked" => "Blocked",
                _ => post.Status // No change if status is not recognized
            };
            _postRepository.UpdatePost(post);

            // Send notification for user
            if (status != null)
            {
                NotificationType notificationType = NotificationType.PostApproved;
                if (post.Status.Equals("Approved"))
                    notificationType = NotificationType.PostApproved;
                else if (post.Status.Equals("Rejected"))
                    notificationType = NotificationType.PostRejected;
                else if (post.Status.Equals("Blocked"))
                    notificationType = NotificationType.PostBlocked;

                string notiTitle = NotificationHelper.GetNotiTitleForUser(notificationType);
                string notiMessage = NotificationHelper.GetNotiMessageForUser(notificationType);

                _notificationRepository.SendNotificationForUser(post.UserID, notiTitle, notiMessage, notificationType);
            }

            return RedirectToPage("/Admin/Forums/Topics/List", new { id = post.Topic.ID });

        }
    }
}
