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
        Task<ICollection<AppPost>> GetAllPosts();
        Task<AppPost> GetPostById(int postId);
        Task<PostDto> AddPost(AppPost post);
        PostDto RemovePost(AppPost post);
        Task<ICollection<AppPost>> GetPostsByUserId(int userId);
        Task<ICollection<AppPost>> GetPostsByUsername(string username);
        //Task<IEnumerable<PostLikesDto>> GetUsersLikesByPostId(int postId);
    }
}