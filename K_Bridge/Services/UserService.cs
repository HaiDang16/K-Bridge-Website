using K_Bridge.Infrastructure;
using K_Bridge.Models;
using K_Bridge.Repositories;

namespace K_Bridge.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public User GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.Session.GetJson<User>("user") ?? null;
            return user;
        }
    }
}
