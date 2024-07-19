using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IReplyRepository
    {
        IQueryable<Reply> Replies { get; }
        void SaveReply(Reply reply);
        IEnumerable<Reply> GetRepliesByPostId(int postId);

    }
}
