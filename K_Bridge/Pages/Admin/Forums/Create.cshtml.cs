using K_Bridge.Models;
using K_Bridge.Repositories;
using K_Bridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Pages.Admin.Forums
{

    public class CreateModel : PageModel
    {
        private readonly ITopicRepository _topicRepository;
        private readonly CodeGenerationService _codeGenerationService;

        public CreateModel(ITopicRepository topicRepository, CodeGenerationService codeGenerationService)
        {
            _topicRepository = topicRepository;
            _codeGenerationService = codeGenerationService;
            NewCode = _codeGenerationService.GenerateNewCode<Topic>("TOPIC");
        }

        public string NewCode { get; set; }

        [BindProperty]
        public int ForumID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập tên chủ đề")]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        public string Description { get; set; }

        public void OnGet(int id)
        {
            ForumID = id;
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            if (_topicRepository.TopicNameExists(Name))
            {
                return new JsonResult(new
                {
                    success = false,
                    errors = new List<string> { "Tên chủ đề đã tồn tại." }
                });
            }

            string newCode = _codeGenerationService.GenerateNewCode<Topic>("TOPIC");

            var newTopic = new Topic
            {
                Code = newCode,
                Name = Name,
                Description = Description,
                ForumID = ForumID,
                Status = "Active"
            };

            try
            {
                _topicRepository.SaveTopic(newTopic);
                return new JsonResult(new { success = true, forumId = ForumID });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, errors = new List<string> { "Có lỗi xảy ra khi lưu chủ đề." } });
            }
        }
    }
}
