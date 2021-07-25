using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Repositories;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly LikesRepository _likesRepository;
        private readonly UserRepository _userRepository;
        public LikesController(LikesRepository likesRepository, UserRepository userRepository)
        {
            _userRepository = userRepository;
            _likesRepository = likesRepository;
        }

        [AllowAnonymous]
        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var sourceUserId = user.Id;
            return null;

        }
/*
        //api/posts/3
        //[Authorize]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AppPost>>> GetPostsByUserId(int id)
        {
            return await _context.Posts.Where(post => post.UserId == id).ToListAsync();
        }


        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<ActionResult<PostDto>> AddPost(PostDto postDto)
        {
            var post = new AppPost
            {
                Title = postDto.Title,
                Content = postDto.Content
            };

            Console.WriteLine(post.Title + "!!!!!!!!!!!!!!");
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return new PostDto
            {
                Title = post.Title,
                Content = post.Content
            };

        }

        private async Task<bool> isPostExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName.Equals(username.ToLower()));
        }
        */
    }
}
