using Facebook_be.Dto;
using Facebook_be.Models;
using Facebook_be.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Facebook_be.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly FacebookDbContext _context;
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;

        public ChatController(IChatRepository chatRepository, FacebookDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }

        [HttpPost("add")]
        public IActionResult AddChatBetweenUser([FromBody] ChatDTO chatDto)
        {
            var newChat = new Chat
            {
                Content = chatDto.Content,
                ReceiverId = chatDto.ReceiverId,
                SenderId = chatDto.SenderId,
                IsRead = chatDto.IsRead,
            };
            var createdChat = _chatRepository.AddChat(newChat);

            return Ok("Success");
        }

        [HttpGet("chatsWithUser")]
        public IActionResult GetChatsWithUser(int userId, int receiverId)
        {
            if (userId == receiverId)
            {
                return BadRequest(new { message = "Cannot fetch chats with yourself." });
            }

            var chats = _chatRepository.GetChatsBetweenUsers(userId, receiverId);

            if (chats == null || !chats.Any())
            {
                return NotFound(new { message = "No chats found." });
            }

            var receiver = _userRepository.GetById(receiverId);

            if (receiver == null)
            {
                return NotFound(new { message = "Receiver not found." });
            }

            var chatDto = chats.Select(chat => new
            {
                id = chat.Id,
                content = chat.Content,
                //receiverFullName = receiver.FullName, 
                //receiverProfileImg = receiver.ProfileImg,
                createAt = chat.CreatedAt,
                isSender = chat.SenderId == userId
            }).ToList();

            return Ok(chatDto);
        }


        [HttpGet("lasted")]
        public IActionResult GetLastedMessageInfo(int userId, string? searchString) // truyền vào id của người đăng nhập
        {
            // Bước 1: Lấy các ID user đã gửi hoặc nhận message (id truyền vào là id người dùng đã đăng nhập) duy nhất
            var contactIds = _context.Chats
                .Where(chat => chat.SenderId == userId || chat.ReceiverId == userId)
                .Select(chat => chat.SenderId == userId ? chat.ReceiverId : chat.SenderId)
                .Distinct()
                .ToList();

            // Bước 2: Lấy thông tin của các id đã lấy được ở trên (người đã nhắn hoặc gửi cho user đăng nhập)
            var contacts = _context.Users
                .Where(user => contactIds.Contains(user.Id))
                .ToList();

            // Bước 3: Lấy đoạn chat cuối cùng với từng người trong trong messenger
            var lastedChats = _context.Chats
                .Where(chat => (chat.SenderId == userId && contactIds.Contains(chat.ReceiverId))
                            || (chat.ReceiverId == userId && contactIds.Contains(chat.SenderId)))
                .GroupBy(chat => chat.SenderId == userId ? chat.ReceiverId : chat.SenderId)
                .Select(group => group.OrderByDescending(chat => chat.CreatedAt).FirstOrDefault())
                .ToList();

            // Bước 4: Kết hợp thông tin đoạn chat cuối cùng và tên người dùng 
            var result = lastedChats.Select(chat => new
            {
                userId = chat.SenderId == userId ? chat.ReceiverId : chat.SenderId,
                fullName = contacts.FirstOrDefault(c => c.Id == (chat.SenderId == userId ? chat.ReceiverId : chat.SenderId))?.FullName,
                lastMessage = chat.Content,
                createAt = chat.CreatedAt,
                isSender = chat.SenderId == userId
            }).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result
                    .Where(r => r.fullName != null && r.fullName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (result == null || !result.Any())
            {
                return NotFound(new { message = "First chat now" });
            }

            return Ok(result);
        }
    }
}