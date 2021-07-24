using System;

namespace API.Entities
{
    public class Follow
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public int FollowerUserId { get; set; }
        public AppUser FollowerUser { get; set; }
        public int FollowedUserId { get; set; }
        public AppUser FollowedUser { get; set; }
    }
}