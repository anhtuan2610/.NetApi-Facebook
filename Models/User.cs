using System;
using System.Collections.Generic;

namespace Facebook_be.Models
{
    public partial class User
    {
        public User()
        {
            ChatReceivers = new HashSet<Chat>();
            ChatSenders = new HashSet<Chat>();
            Comments = new HashSet<Comment>();
            FriendshipUserId1Navigations = new HashSet<Friendship>();
            FriendshipUserId2Navigations = new HashSet<Friendship>();
            Likes = new HashSet<Like>();
            Notifications = new HashSet<Notification>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? FullName { get; set; }
        public string? ProfileImg { get; set; }
        public string? CoverImg { get; set; }
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Chat> ChatReceivers { get; set; }
        public virtual ICollection<Chat> ChatSenders { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Friendship> FriendshipUserId1Navigations { get; set; }
        public virtual ICollection<Friendship> FriendshipUserId2Navigations { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
