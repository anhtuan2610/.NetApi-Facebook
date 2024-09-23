using System;
using System.Collections.Generic;

namespace Facebook_be.Models
{
    public partial class Chat
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsRead { get; set; }

        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
