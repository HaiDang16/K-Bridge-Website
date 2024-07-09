namespace K_Bridge.Models
{
    public interface IForumRepository
    {
        IQueryable<Forum> Forums { get; }
    }
}
