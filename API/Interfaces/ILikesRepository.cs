using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<Like> GetPostLike(int userId, int postId);
        Boolean RemoveLike(Like like);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<IEnumerable<AppPost>> GetPostsLikedByUserId(int userId);
        Task<IEnumerable<PostLikesDto>> GetUsersLikesByPostId(int postId);
    }
}