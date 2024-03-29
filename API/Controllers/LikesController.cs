﻿using System;
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
    public class LikesController : BaseApiController
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public LikesController(ILikesRepository likesRepository
                            , IUserRepository userRepository
                            , IPostRepository postRepository
                            , IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _likesRepository = likesRepository;
        }

        [Authorize]
        [HttpPost("{postId}")]
        public async Task<ActionResult> AddLike(int postId)
        {
            Console.WriteLine("username = " + User.GetUsername());
            var user = await _userRepository.GetUserWithPostsByUsernameAsync(User.GetUsername());
            var sourceUserId = user.Id;
            AppPost post = await _postRepository.GetPostById(postId);

            if (post == null) return NotFound();

            Like like = await _likesRepository.GetPostLike(sourceUserId, postId);

            if (like != null)
            {
                //TODO: implement DISLIKE 
                return BadRequest("Already liked this post");
            }

            like = new Like{
                LikedPostId = postId,
                LikesUserId = sourceUserId
            };

            if (post.LikedByUsers == null) post.LikedByUsers = new List<Like>();
            post.LikedByUsers.Add(like);
            if (user.LikedPosts == null) user.LikedPosts = new List<Like>();
            user.LikedPosts.Add(like);

            if(await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to like the post");
        }

    

        //[Route(":postId")]
        [Authorize]
        [HttpDelete("{postId}")]
        public async Task<ActionResult<Boolean>> RemoveLikeByPostId(int postId)
        {
            var user = await _userRepository.GetUserWithPostsByUsernameAsync(User.GetUsername());
            var userId = user.Id;
            
            Like like = await _likesRepository.GetPostLike(userId, postId);
            if(like == null) return BadRequest("You don't like this post");
            
            Boolean isRemoved = _likesRepository.RemoveLike(like);
            return isRemoved;
        }
    //api/posts/3
    //[Authorize]
    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsLikedByUserId(int userId)
    {
        var posts = await _likesRepository.GetPostsLikedByUserId(userId);
        if (posts == null) return BadRequest("User didn't like any post");
        IEnumerable<PostDto> postsToReturn = _mapper.Map<IEnumerable<PostDto>>(posts);
        /*var postLikesDto = new UserPostLikesDto{
            Posts = postsToReturn
        };*/
        return Ok(postsToReturn);
    }
}
}
