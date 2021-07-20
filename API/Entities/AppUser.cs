using System.Collections.Generic;
using System;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public DateTime TimeCreated { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<AppPost> Posts { get; set; }
    }
}