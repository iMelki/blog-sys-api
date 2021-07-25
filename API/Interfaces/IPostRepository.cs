using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPostRepository
    {
        Task<AppPost> GetPostById(int postId);
        Task<ICollection<AppPost>> GetPostsByUserId(int userId);
        //Task<IEnumerable<PostLikesDto>> GetUsersLikesByPostId(int postId);
    }
}