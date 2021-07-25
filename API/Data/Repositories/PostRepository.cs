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
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppPost> GetPostById(int postId)
        {
            return await _context.Posts.FindAsync(postId);
        }

        public async Task<Like> GetPostLike(int userId, int postId)
        {
            return await _context.Likes.FindAsync(userId, postId);
        }

        public async Task<ICollection<AppPost>> GetPostsByUserId(int userId)
        {
            return await _context.Posts.Where(post => post.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<AppPost>> GetPostsLikedByUserId(int userId)
        {
            IEnumerable<Like> likesWithPost = await _context.Likes
                .Where(x => x.LikesUserId == userId)
                .Include(x => x.LikedPost).ToListAsync();
            IEnumerable<AppPost> posts = likesWithPost.Select(l => l.LikedPost);
            return posts;
            //      .Include(user => user.LikedPosts)
            //        .FirstOrDefaultAsync(user => user.Id == userId);
        }

        public Task<IEnumerable<PostLikesDto>> GetUsersLikesByPostId(int postId)
        {
            throw new NotImplementedException();
        }
    }
}