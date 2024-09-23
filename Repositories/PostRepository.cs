using Facebook_be.Models;

namespace Facebook_be.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly FacebookDbContext _context;
        public PostRepository(FacebookDbContext context)
        {
            _context = context;
        }
        public Post CreatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllPost()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostByUserId(int id)
        {
            return _context.Posts.Where(p=> p.UserId == id);
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> GetPosts()
        {
            throw new NotImplementedException();
        }
    }
}
