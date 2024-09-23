using Facebook_be.Models;

namespace Facebook_be.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly FacebookDbContext _context;

        public CommentRepository(FacebookDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Comment> GetCommentsByPostId(int postId)
        {
            return _context.Comments.Where(c => c.PostId == postId);
        }
    }
}
