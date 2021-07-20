using System;

namespace API.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int LikesUserId { get; set; }
        public AppUser LikesAppUser { get; set; }
        public int LikedPostId { get; set; }
        public AppPost LikedAppPost { get; set; }
    }
}