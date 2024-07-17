
using K_Bridge.Repositories;


﻿using Microsoft.AspNetCore.Mvc;
using K_Bridge.Attributes;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Bridge.Pages.Admin
{
    [AdminAuth]
    public class DashboardModel : PageModel
    {
        private readonly IKBridgeRepository _repository;

        public DashboardModel(IKBridgeRepository repository)
        {
            _repository = repository;
        }

        public int TotalUsers { get; set; }
        public int TotalTopics { get; set; }
        public int TotalPosts { get; set; }
        public int RecentUsers { get; set; }
        public int RecentTopics { get; set; }
        public int RecentPosts { get; set; }

        public void OnGet()
        {
            TotalUsers = _repository.GetTotalUsers();
            TotalTopics = _repository.GetTotalTopics();
            TotalPosts = _repository.GetTotalPosts();
            RecentUsers = _repository.GetRecentUsers();
            RecentTopics = _repository.GetRecentTopics();
            RecentPosts = _repository.GetRecentPosts();
        }
    }
}
