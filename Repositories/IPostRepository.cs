using Facebook_be.Models;

namespace Facebook_be.Repositories
{
    public interface IPostRepository
    {
        Post CreatePost(Post post);
        IEnumerable<Post> GetAllPost();
        IEnumerable<Post> GetPosts();
        IEnumerable<Post> GetPostByUserId(int id);
    }
}
