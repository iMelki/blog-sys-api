using System.Collections.Generic;
using System;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public IEnumerable<AppPost> Posts { get; set; }
        public ICollection<Like> LikedPosts { get; set; }
        public IEnumerable<Follow> FollowedByUsers { get; set; }
        public IEnumerable<Follow> FollowsUsers { get; set; }
    }
}