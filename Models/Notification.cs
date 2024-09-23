﻿using System;
using System.Collections.Generic;

namespace Facebook_be.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Content { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
