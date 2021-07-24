using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}