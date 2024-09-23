using System;
using System.Collections.Generic;

namespace Facebook_be.Models
{
    public partial class Like
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
