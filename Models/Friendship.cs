using System;
using System.Collections.Generic;

namespace Facebook_be.Models
{
    public partial class Friendship
    {
        public int Id { get; set; }
        public int? UserId1 { get; set; }
        public int? UserId2 { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? UserId1Navigation { get; set; }
        public virtual User? UserId2Navigation { get; set; }
    }
}
