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
        [Required]
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
                return Page();
            }

            string newCode = _codeGenerationService.GenerateNewCode<Topic>("TOPIC");


            var newTopic = new Topic
            {
                Code = newCode,
                Name = Name,
                Description = Description,
                ForumID = ForumID
            };

            _topicRepository.SaveTopic(newTopic);

            return RedirectToPage("/Admin/Forums/List", new { id = ForumID });
        }
    }
}
