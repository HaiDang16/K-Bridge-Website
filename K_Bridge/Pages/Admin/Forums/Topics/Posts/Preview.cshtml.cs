using K_Bridge.Models;
using K_Bridge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K_Bridge.Pages.Admin.Forums.Topics.Posts
{
    public class PreviewModel : PageModel
    {
        private readonly IPostRepository _postRepository;
        private readonly IKBridgeRepository _repository;


        public PreviewModel(IPostRepository postRepository, IKBridgeRepository repository)
        {
            _postRepository = postRepository;
            _repository = repository;
        }


        public Post Post { get; set; }
        public void OnGet(int id)
        {
            var postDetails = _postRepository.GetPostByID(id);
            Post = postDetails;
        }
    }
}
