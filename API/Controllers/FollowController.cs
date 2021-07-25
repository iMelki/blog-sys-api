using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Repositories;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Authorize]
    public class FollowController : BaseApiController
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IFollowRepository _followRepository;
        private readonly IMapper _mapper;
        public FollowController(ILikesRepository likesRepository
                            , IUserRepository userRepository
                            , IPostRepository postRepository
                            , IFollowRepository followRepository
                            , IMapper mapper)
        {
            _likesRepository = likesRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _followRepository = followRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("{username}")]
        public async Task<ActionResult> FollowByUsername(string username)
        {
            AppUser user = await _userRepository.GetUserWithPostsByUsernameAsync(User.GetUsername());
            int sourceUserId = user.Id;
            user = await _followRepository.GetUserWithFollows(sourceUserId);
            
            AppUser destUser = await _userRepository.GetUserWithPostsByUsernameAsync(username);
            if (destUser == null) return NotFound("Username doesn't exist");
            int destUserId = destUser.Id;
            destUser = await _followRepository.GetUserWithFollows(destUserId);

            if (sourceUserId == destUserId) return BadRequest("You cannot follow yourself");

            Follow follow = await _followRepository.GetFollow(sourceUserId, destUserId);

            if (follow != null)
            {
                //TODO: implement UNFOLLOW
                return BadRequest("You already follow this user");
            }

            follow = new Follow{
                FollowedUserId = destUserId,
                FollowerUserId = sourceUserId
            };

            //if (destUser.FollowedByUsers == null) destUser.FollowedByUsers = new List<Follow>();
            destUser.FollowedByUsers.Add(follow);
            //if (user.FollowsUsers == null) user.FollowsUsers = new List<Follow>();
            user.FollowsUsers.Add(follow);

            if(await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to follow the user");
        }

        //[Route(":username")]
        [Authorize]
        [HttpDelete("{username}")]
        public async Task<ActionResult<Boolean>> UnfollowByUsername(string username)
        {
            var user = await _userRepository.GetUserWithPostsByUsernameAsync(User.GetUsername());
            var userId = user.Id;

            AppUser destUser = await _userRepository.GetUserWithPostsByUsernameAsync(username);
            if (destUser == null) return NotFound("Username doesn't exist");
            int destUserId = destUser.Id;
            
            Follow follow = await _followRepository.GetFollow(userId, destUserId);
            if(follow == null) return BadRequest("You're not following this user");
            
            Boolean isRemoved = _followRepository.Unfollow(follow);
            return isRemoved;
        }
        
        //api/follow/:predicate
        [Authorize]
        [HttpGet("{predicate}")]
        public async Task<ActionResult<IEnumerable<int>>> GetUserFollows(string predicate)
        {
            var user = await _userRepository.GetUserWithPostsByUsernameAsync(User.GetUsername());
            var userId = user.Id;
            var users = await _followRepository.GetUserFollows(predicate, userId);
            if (users == null) return BadRequest("NONE");
            IEnumerable<int> userssToReturn = users.Select(u => u.Id);
            return Ok(userssToReturn);
        }
        
    }
}
