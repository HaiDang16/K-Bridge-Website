using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Pages.Admin.Forums.Topics
{
    public class ListModel : PageModel
    {
        private readonly IKBridgeRepository _repository;
        private readonly IPostRepository _postRepository;
        public ListModel(IKBridgeRepository repository, IPostRepository postRepository)
        {
            _repository = repository;
            _postRepository = postRepository;
        }
        public Topic Topic { get; set; }
        public List<Post> Posts { get; set; }
        public int ForumID { get; set; }
        public string ForumName { get; set; }

        public int PageIndex { get; set; } = 1; // Trang hiện tại
        public int PageSize { get; set; } = 8; // Số lượng bản ghi trên mỗi trang
        public int TotalPages { get; set; } // Tổng số trang
        public List<int> DisplayPages { get; set; } // Các trang để hiển thị

        public void OnGet(int id, int? pageIndex = 1)
        {
            PageIndex = pageIndex ?? 1;

            Posts = _postRepository.GetAllPostsWithTopicPaging(id, PageIndex, PageSize);

            // Tính toán tổng số trang
            var totalRecords = _postRepository.CountPostWithTopic(id);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            TotalPages = totalPages;

            DisplayPages = GenerateDisplayPages(PageIndex, totalPages);

            Topic = _repository.Topics
               .Include(f => f.Posts)
               .ThenInclude(p => p.User)
               .Include(f => f.Forum)
               .FirstOrDefault(f => f.ID == id);

            ForumID = Topic.ForumID;
            ForumName = Topic.Forum.Name;
        }
        private List<int> GenerateDisplayPages(int currentPage, int totalPages)
        {
            const int maxDisplayPages = 3; // Số lượng trang tối đa để hiển thị

            var displayPages = new List<int>();

            // Nếu tổng số trang nhỏ hơn hoặc bằng maxDisplayPages, hiển thị tất cả các trang
            if (totalPages <= maxDisplayPages)
            {
                for (int i = 1; i <= totalPages; i++)
                {
                    displayPages.Add(i);
                }
            }
            else
            {
                // Hiển thị trang đầu tiên
                displayPages.Add(1);

                // Hiển thị các trang xung quanh trang hiện tại
                var start = Math.Max(2, currentPage - 1);
                var end = Math.Min(currentPage + 1, totalPages - 1);

                // Hiển thị dấu "..."
                if (start > 2)
                    displayPages.Add(-1);

                for (int i = start; i <= end; i++)
                {
                    displayPages.Add(i);
                }

                // Hiển thị trang cuối cùng
                if (end < totalPages - 1)
                    displayPages.Add(-1);

                displayPages.Add(totalPages);
            }
            return displayPages;
        }

    }
}
