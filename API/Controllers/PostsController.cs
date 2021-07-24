using System;
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

namespace API.Controllers
{
    public class PostsController : BaseApiController
    {
        private readonly DataContext _context;
        public PostsController(DataContext context)
        {
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
    }
}
