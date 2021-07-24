using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Data.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;

        public LikesRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<UserPostLikesDto>> GetPostsLikedByUserId(int userId)
        {
            /*
            var posts = _context.Posts.OrderBy().AsQu
            List<TaskItem> items = await _context.Where(item => (!(item.IsCompleted))).ToListAsync();
            List<ItemDto> itemDtos = new List<ItemDto>();
            items.ForEach(item => itemDtos.Add(new ItemDto {Id = item.Id, Caption = item.Caption }));
            return itemDtos;
            await _context.Likes
                .Include()
            */
            return null;
        }
        public Task<IEnumerable<PostLikesDto>> GetUsersLikesByPostId(int postId)
        {
            return null;
        }

        
    }
}