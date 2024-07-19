using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Pages.Admin.Forums
{
    public class TopicModel : PageModel
    {
        private readonly IKBridgeRepository _repository;


        public TopicModel(IKBridgeRepository repository)
        {
            _repository = repository;
        }
        public Topic Topic { get; set; }
        public IList<Post> Posts { get; set; }
        public void OnGet(int id)
        {
            Topic = _repository.Topics
               .Include(f => f.Posts)
               .ThenInclude(p => p.User)
               .FirstOrDefault(f => f.ID == id);

            Posts = Topic?.Posts.ToList();
        }
    }
}
