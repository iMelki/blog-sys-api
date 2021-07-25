using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PostDto
    {
        //public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        //public DateTime TimePosted { get; set; }
    }
}