using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly DataContext _context;
        public FollowRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Follow> GetFollow(int followerUserId, int followedUserId)
        {
            return await _context.Follows.FindAsync(followerUserId, followedUserId);
        }

        public async Task<IEnumerable<AppUser>> GetUserFollows(string predicate, int userId)
        {
            var users = _context.Users
                    .OrderBy(u => u.UserName)
                    .AsQueryable();
            var follows = _context.Follows.AsQueryable();
            
            Console.WriteLine("predicate = " + predicate);
            if(predicate == "follows") //users followed by userId
            {
                follows = follows.Where(f => f.FollowerUserId == userId);
                users = follows.Select(f => f.FollowedUser);
            }

            if(predicate == "followers") //users followed by userId
            {
                follows = follows.Where(f => f.FollowedUserId == userId);
                users = follows.Select(f => f.FollowerUser);
            }

            return await users.ToListAsync();
        }

        public async Task<AppUser> GetUserWithFollows(int userId)
        {
            return await _context.Users
                        .Include(u => u.FollowsUsers)
                        .Include(u => u.FollowedByUsers)
                        .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public bool Unfollow(Follow follow)
        {
            try{
                _context.Follows.Remove(follow);                
                _context.SaveChanges();
                return true;
            }catch{
                return false;
            }
        }
    }
}