﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using API.Interfaces;
using API.Extensions;

namespace API.Controllers
{
    [Authorize]
    public class PostsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        public PostsController(DataContext context, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppPost>>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        //api/posts/3
        //[Authorize]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AppPost>>> GetPostsByUserId(int id)
        {
            return await _context.Posts.Where(post => post.UserId == id).ToListAsync();
        }


        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<PostDto>> AddPost(PostDto postDto)
        {
            //var userId = (User.Identity.Name);
            //Console.WriteLine("User.GetUsername() = " + User.GetUsername());
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            Console.WriteLine("user = " + user.ToString());
            var userId = user.Id;
            Console.WriteLine("ID = " + userId);
            var post = new AppPost
            {
                UserId = userId,
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
    }
}
