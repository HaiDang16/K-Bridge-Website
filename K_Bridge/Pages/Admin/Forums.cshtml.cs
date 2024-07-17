using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Pages.Admin
{
    public class ForumsModel : PageModel
    {
        private readonly IForumRepository _forumRepository;
        private readonly IKBridgeRepository _repository;


        public ForumsModel(IForumRepository forumRepository, IKBridgeRepository repository)
        {
            _forumRepository = forumRepository;
            _repository = repository;
        }

        public Forum Forum { get; set; }
        public IList<Topic> Topics { get; set; }
        public void OnGet(int id)
        {
            Forum = _repository.Forums
               .Include(f => f.Topics)
               .FirstOrDefault(f => f.ID == id);

            Topics = Forum?.Topics.ToList();
        }
    }
}
