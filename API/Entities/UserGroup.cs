using System.Collections.Generic;
using System;

namespace API.Entities
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<AppUser> Users { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
    }
}