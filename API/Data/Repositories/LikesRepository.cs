using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;

        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Like> GetPostLike(int userId, int postId)
        {
            return await _context.Likes.FindAsync(userId, postId);
        }

        public async Task<IEnumerable<AppPost>> GetPostsLikedByUserId(int userId)
        {
            IEnumerable<Like> likesWithPost = await _context.Likes
                .Where(x => x.LikesUserId == userId)
                .Include(x => x.LikedPost).ToListAsync();
            if (likesWithPost.Count() < 1) return null;
            IEnumerable<AppPost> posts = likesWithPost.Select(l => l.LikedPost);
            return posts;
            //      .Include(user => user.LikedPosts)
            //        .FirstOrDefaultAsync(user => user.Id == userId);
        }

        public Task<IEnumerable<PostLikesDto>> GetUsersLikesByPostId(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                        .Include(u => u.LikedPosts)
                        .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public bool RemoveLike(Like like)
        {
            try{
                _context.Likes.Remove(like);                
                _context.SaveChanges();
                return true;
            }catch{
                return false;
            }
        }
    }
}