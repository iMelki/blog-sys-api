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
        Task<IEnumerable<UserPostLikesDto>> GetPostsLikedByUserId(int userId);
        Task<IEnumerable<PostLikesDto>> GetUsersLikesByPostId(int postId);
    }
}