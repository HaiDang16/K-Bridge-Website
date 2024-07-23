using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Pages.Admin.Forums
{
    public class ListModel : PageModel
    {
        private readonly IForumRepository _forumRepository;
        private readonly IKBridgeRepository _repository;
        private readonly ITopicRepository _topicRepository;


        public ListModel(IForumRepository forumRepository, IKBridgeRepository repository, ITopicRepository topicRepository)
        {
            _forumRepository = forumRepository;
            _repository = repository;
            _topicRepository = topicRepository;
        }

        public Forum Forum { get; set; }
        [BindProperty]
        public int ForumID { get; set; }
        public List<Topic> Topics { get; set; }

        public int PageIndex { get; set; } = 1; // Trang hiện tại
        public int PageSize { get; set; } = 1; // Số lượng bản ghi trên mỗi trang
        public int TotalPages { get; set; } // Tổng số trang
        public List<int> DisplayPages { get; set; } // Các trang để hiển thị
        public void OnGet(int id, int? pageIndex = 1)
        {
            PageIndex = pageIndex ?? 1;

            Topics = _topicRepository.GetAllTopicsWithForumPaging(id, PageIndex, PageSize);

            // Tính toán tổng số trang
            var totalRecords = _topicRepository.CountTopicWithForum(id);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            TotalPages = totalPages;

            DisplayPages = GenerateDisplayPages(PageIndex, totalPages);

            ForumID = id;

            Forum = _repository.Forums
             .Include(f => f.Topics)
             .ThenInclude(p => p.Posts)
             .FirstOrDefault(f => f.ID == id);
        }
        public IActionResult OnPostDelete(int id)
        {
            var topic = _topicRepository.GetTopicById(id);
            if (topic == null)
            {
                return NotFound();
            }

            _topicRepository.SetTopicStatusInactive(id);

            // Redirect back to the list page
            return RedirectToPage("/Admin/Forums/List", new { id = ForumID });
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
