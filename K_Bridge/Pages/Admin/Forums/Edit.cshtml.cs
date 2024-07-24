using K_Bridge.Models;
using K_Bridge.Repositories;
using K_Bridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Pages.Admin.Forums
{
    public class EditModel : PageModel
    {
        private readonly ITopicRepository _topicRepository;
        private readonly CodeGenerationService _codeGenerationService;

        public EditModel(ITopicRepository topicRepository, CodeGenerationService codeGenerationService)
        {
            _topicRepository = topicRepository;
            _codeGenerationService = codeGenerationService;
        }
        [BindProperty]
        public string Code { get; set; }

        [BindProperty]
        public int ForumID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập tên chủ đề")]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        public string Description { get; set; }
        [BindProperty]
        public int TopicID { get; set; }

        public void OnGet(int id)
        {
            var topic = _topicRepository.GetTopicById(id);
            if (topic != null)
            {
                Code = topic.Code;
                Name = topic.Name;
                Description = topic.Description;
                ForumID = topic.ForumID;
                TopicID = id;
            }
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            // Kiểm tra sự tồn tại của tên chủ đề (trừ chủ đề hiện tại)
            if (_topicRepository.TopicNameExists(Name) && _topicRepository.GetTopicById(id).Name != Name)
            {
                return new JsonResult(new
                {
                    success = false,
                    errors = new List<string> { "Tên chủ đề đã tồn tại." }
                });
            }

            var existingTopic = _topicRepository.GetTopicById(TopicID);
            if (existingTopic == null)
            {
                return new JsonResult(new { success = false, errors = new List<string> { "Chủ đề không tồn tại." } });
            }

            existingTopic.Name = Name;
            existingTopic.Description = Description;

            try
            {
                _topicRepository.UpdateTopic(existingTopic);
                return new JsonResult(new { success = true, forumId = ForumID });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, errors = new List<string> { "Có lỗi xảy ra khi cập nhật chủ đề." } });
            }
        }

    }
}
