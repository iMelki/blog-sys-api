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
        private readonly IPostRepository _postRepository;
        public PostsController(DataContext context
                            , IUserRepository userRepository
                            , IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ICollection<AppPost>>> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPosts();
            return Ok(posts);
        }

        //api/posts/3
        //[Authorize]
        [AllowAnonymous]
        [HttpGet("{userId}")]
        public async Task<ActionResult<ICollection<AppPost>>> GetPostsByUserId(int userId)
        {
            var posts = await _postRepository.GetPostsByUserId(userId);
            return Ok(posts);
        }

        //api/posts/avi
        //[Authorize]
        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<ActionResult<ICollection<AppPost>>> GetPostsByUsername(string username)
        {
            var posts = await _postRepository.GetPostsByUsername(username);
            return Ok(posts);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<PostDto>> AddPost(PostDto postDto)
        {
            //var userId = (User.Identity.Name);
            //Console.WriteLine("User.GetUsername() = " + User.GetUsername());
            var user = await _userRepository.GetUserWithPostsByUsernameAsync(User.GetUsername());
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
            PostDto postDetails = await _postRepository.AddPost(post);
            return postDetails;

        }

        //[Route("remove/:postId")]
        [Authorize]
        [HttpDelete("remove/{postId}")]
        public async Task<ActionResult<PostDto>> RemovePost(int postId)
        {
            var user = await _userRepository.GetUserWithPostsByUsernameAsync(User.GetUsername());
            var userId = user.Id;
            
            AppPost post = await _postRepository.GetPostById(postId);
            if(userId != post.UserId) return Unauthorized("Not Your Post");
            
            PostDto postDetails = _postRepository.RemovePost(post);
            return postDetails;
        }

        private async Task<bool> isPostExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName.Equals(username.ToLower()));
        }
    }
}
