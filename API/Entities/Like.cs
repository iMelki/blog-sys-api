using System;

namespace API.Entities
{
    public class Like
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public int LikesUserId { get; set; }
        public AppUser LikesUser { get; set; }
        public int LikedPostId { get; set; }
        public AppPost LikedPost { get; set; }
    }
}