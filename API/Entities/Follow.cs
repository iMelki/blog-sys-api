using System;

namespace API.Entities
{
    public class Follow
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int FollowerUserId { get; set; }
        public AppUser FollowerAppUser { get; set; }
        public int FollowedUserId { get; set; }
        public AppUser FollowedAppUser { get; set; }
    }
}