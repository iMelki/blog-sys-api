using System;

namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SenderUserId { get; set; }
        public AppUser SenderAppUser { get; set; }
        public int RecieverUserId { get; set; }
        public AppUser RecieverAppUser { get; set; }
        public DateTime TimePosted { get; set; }
    }
}