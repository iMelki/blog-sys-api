using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class PostLikesDto
    {
        [Required]
        public IEnumerable<string> LikedUsernames{ get; set; }
    }
}