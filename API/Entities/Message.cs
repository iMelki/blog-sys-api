using System;

namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }

        // EF Relations:
        public int SenderUserId { get; set; }
        public string SenderUserName { get; set; }
        public AppUser SenderAppUser { get; set; }
        public int RecipientUserId { get; set; }
        public string RecipientUserName { get; set; }
        public AppUser RecipientAppUser { get; set; }
        
        // Message specific props:
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime TimePosted { get; set; } = DateTime.UtcNow;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}