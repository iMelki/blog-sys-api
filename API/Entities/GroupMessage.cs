using System;

namespace API.Entities
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int SenderUserId { get; set; }
        public AppUser SenderAppUser { get; set; }
        public int GroupId { get; set; }
        public UserGroup UserGroup { get; set; }
        public DateTime TimePosted { get; set; } = DateTime.Now;
    }
}