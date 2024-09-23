using Facebook_be.Models;

namespace Facebook_be.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly FacebookDbContext _context;

        public ChatRepository(FacebookDbContext context)
        {
            _context = context;
        }

        public Chat AddChat(Chat chat)
        {
            _context.Chats.Add(chat);
            _context.SaveChanges();
            return chat;
        }

        public void DeleteChat(int chatId)
        {
            throw new NotImplementedException();
        }

        public List<Chat> GetChatsBetweenUsers(int loggedInUserId, int receiverId)
        {
            return _context.Chats
                .Where(chat => (chat.SenderId == loggedInUserId && chat.ReceiverId == receiverId)
                            || (chat.SenderId == receiverId && chat.ReceiverId == loggedInUserId))
                .OrderBy(chat => chat.CreatedAt)
                .ToList();
        }

        public IEnumerable<Chat> GetChatsByUserId(int UserId)
        {
            return _context.Chats.Where(c => c.SenderId == UserId || c.ReceiverId == UserId).ToList();
        }

        public Chat GetLastedChat(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateChat(int chatId)
        {
            throw new NotImplementedException();
        }
    }
}
