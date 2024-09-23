using Facebook_be.Models;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Facebook_be.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<int, string> _connections = new(); // phải dùng static nghĩa là nó sẽ chia sẻ giữa tất cả các instance của ChatHub. Điều này cho phép bạn lưu trữ tất cả các kết nối của người dùng.

        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (int.TryParse(userId, out int parsedUserId)) // parse int sang string
            {
                _connections[parsedUserId] = Context.ConnectionId;
            }
            Console.WriteLine("ConnectionId: " + userId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(int senderId, int receiverId, string newMessage)
        {
            if (_connections.TryGetValue(receiverId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", newMessage);
                Console.WriteLine("Message send by senderId: " + senderId + " and receiver is receiverId: " + receiverId);
            }
            else
            {
                Console.WriteLine($"No connection found for user: {receiverId}");
            }
        }
    }
}
