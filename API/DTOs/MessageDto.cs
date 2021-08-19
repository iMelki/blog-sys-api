using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class MessageDto
    {
        //[Required]
        //public string Username { get; set; }
        public int Id { get; set; }
        public int SenderUserId { get; set; }
        public string SenderUsername { get; set; }
        public int RecipientUserId { get; set; }
        public string RecipientUsername { get; set; }
        
        // Message specific props:
        [Required]
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime TimePosted { get; set; }
    }
}