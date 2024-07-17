using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IGlobalChatRepository
    {
        IQueryable<GlobalChat> GlobalChats { get; }
        IEnumerable<GlobalChat> GetRecentMessages();
        void AddMessage(GlobalChat message);
        void DeleteOldMessages(DateTime threshold);
    }
}
