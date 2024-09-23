using Facebook_be.Models;

namespace Facebook_be.Repositories
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsByPostId(int postId);
    }
}
