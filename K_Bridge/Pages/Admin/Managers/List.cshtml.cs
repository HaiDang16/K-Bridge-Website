using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Bridge.Pages.Admin.Manager
{
    public class ListModel : PageModel
    {
        private readonly IAdminAccountRepository _adminAccountRepository;

        public ListModel(IAdminAccountRepository adminAccountRepository)
        {
            _adminAccountRepository = adminAccountRepository;
        }

        public List<Admin_Accounts> Admin_Accounts { get; set; }
        public int PageIndex { get; set; } = 1; // Trang hiện tại
        public int PageSize { get; set; } = 8; // Số lượng bản ghi trên mỗi trang
        public int TotalPages { get; set; } // Tổng số trang
        public List<int> DisplayPages { get; set; } // Các trang để hiển thị

        public void OnGet(int? pageIndex = 1)
        {
            PageIndex = pageIndex ?? 1;

            Admin_Accounts = _adminAccountRepository.GetAllAdminAccountsPaging(PageIndex, PageSize);

            // Tính toán tổng số trang
            var totalRecords = _adminAccountRepository.CountAdminAccount();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            TotalPages = totalPages;

            DisplayPages = GenerateDisplayPages(PageIndex, totalPages);
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
        public IActionResult OnPostDelete(int id)
        {
            var admin = _adminAccountRepository.GetAdminAccountById(id);
            if (admin == null)
            {
                return NotFound();
            }

            _adminAccountRepository.SetAdminAccountStatusInactive(id);

            // Redirect back to the list page
            return RedirectToPage("/Admin/Managers/List");
        }

        public IActionResult OnPostUnlock(int id)
        {
            var admin = _adminAccountRepository.GetAdminAccountById(id);
            if (admin == null)
            {
                return NotFound();
            }
            _adminAccountRepository.SetAdminAccountStatusActive(id);
            // Redirect back to the list page
            return RedirectToPage("/Admin/Managers/List");
        }
    }
}
