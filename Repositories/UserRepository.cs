using Facebook_be.Models;

namespace Facebook_be.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FacebookDbContext _context;
        public UserRepository(FacebookDbContext context)
        {
            _context = context;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);
            //user.Id = _context.SaveChanges(); // khi savechange sẽ trả về id của user vừa tạo
            _context.SaveChanges();
            return user;
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
