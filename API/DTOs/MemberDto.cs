using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class MemberDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime TimeCreated { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
        public IEnumerable<Like> LikedPosts { get; set; }
        public IEnumerable<Follow> FollowedByUsers { get; set; }
        public IEnumerable<Follow> FollowsUsers { get; set; }
    }
}