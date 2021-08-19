using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Posts)
                .ToListAsync();
        }
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .FindAsync(id); 
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<AppUser> GetUserWithPostsByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Posts)
                .SingleOrDefaultAsync(x => x.UserName == username); 
        }

    }
}