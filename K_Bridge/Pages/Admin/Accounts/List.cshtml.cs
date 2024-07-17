using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Bridge.Pages.Admin.Accounts
{
    public class ListModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public ListModel(IUserRepository repository)
        {
            _userRepository = repository;
            Accounts = new List<User>();
          
        }
        public List<User> Accounts { get; set; }

        public int PageIndex { get; set; } = 1; // Trang hiện tại
        public int PageSize { get; set; } = 8; // Số lượng bản ghi trên mỗi trang
        public int TotalPages { get; set; } // Tổng số trang

        public List<int> DisplayPages { get; set; } // Các trang để hiển thị
        public void OnGet(int? pageIndex = 1)
        {
            PageIndex = pageIndex ?? 1;

            Accounts = _userRepository.GetAllUsersPaging(PageIndex, PageSize);

            // Tính toán tổng số trang
            var totalRecords = _userRepository.CountUser();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            TotalPages = totalPages;

            DisplayPages = GenerateDisplayPages(PageIndex, totalPages);
        }
        private List<int> GenerateDisplayPages(int currentPage, int totalPages)
        {
            const int maxDisplayPages = 4; // Số lượng trang tối đa để hiển thị

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
                var start = Math.Max(2, currentPage - 2);
                var end = Math.Min(currentPage + 2, totalPages - 1);

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
        public IActionResult OnPostDelete(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(id);

            // Redirect back to the list page
            return RedirectToPage("/Admin/Accounts/List");
        }
    }
}