using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IFollowRepository
    {
        Task<Follow> GetFollow(int followerUserId, int followedUserId);
        Boolean Unfollow(Follow follow);
        Task<AppUser> GetUserWithFollows(int userId);
        Task<IEnumerable<AppUser>> GetUserFollows(string predicate, int userId);
    }
}