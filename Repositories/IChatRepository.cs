using Facebook_be.Models;

namespace Facebook_be.Repositories
{
    public interface IChatRepository
    {
        Chat AddChat(Chat chat);
        void DeleteChat(int chatId);
        public List<Chat> GetChatsBetweenUsers(int loggedInUserId, int receiverId);
        void UpdateChat(int chatId);
        IEnumerable<Chat> GetChatsByUserId(int UserId);
        Chat GetLastedChat(int Id);
    }
}
