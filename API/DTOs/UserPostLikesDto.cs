using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class UserPostLikesDto
    {
        
        [Required]
        public IEnumerable<PostDto> Posts{ get; set; }
    }
}