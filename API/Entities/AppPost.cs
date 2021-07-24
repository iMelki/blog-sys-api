using System.Collections.Generic;
using System;

namespace API.Entities
{
    public class AppPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public IEnumerable<Like> LikedByUsers { get; set; }
        public DateTime TimePosted { get; set; } = DateTime.Now;
    }
}