using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.BlogPost;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/blogpost")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepo;
        private readonly IImageRepository _imageRepo;
        private readonly IEmailRepository _emailRepo;
        private readonly ISubscriberRepository _subscriberRepo;
        private readonly ApplicationDBContext _context;

        public BlogPostController(IBlogPostRepository blogPostRepo, IImageRepository imageRepo, ApplicationDBContext context, IEmailRepository emailRepo, ISubscriberRepository subscriberRepo)
        {
            _blogPostRepo = blogPostRepo;
            _imageRepo = imageRepo;
            _context = context;
            _emailRepo = emailRepo;
            _subscriberRepo = subscriberRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            // this will only be called when searching for blogposts thus only need to return previews
            var posts = await _blogPostRepo.GetAllAsync(query);
            var blogPostPreviewDtos = posts.Select(p => p.ToBlogPostPreviewDto()).ToList();
            return Ok(blogPostPreviewDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            var post = await _blogPostRepo.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post.ToBlogPostDto());

        }

        [HttpPost]
        [TokenAuth]
        public async Task<IActionResult> Create(CreateBlogPostDto createBlogPostDto)
        {
            try
            {
                // try to find trail by TrailId. FindAsync returns null if can't find.
                var trail = await _context.Trails.FindAsync(createBlogPostDto.TrailId);
                // Create blogpost
                var blogPost = createBlogPostDto.FromCreateToBlogPost(trail);

                // save to db
                var blogpost = await _blogPostRepo.CreateAsync(blogPost);
                
                // get subscribers  
                var subscribers = await _subscriberRepo.GetAllAsync();
                // send out emails without awaiting (works in background)
                _ = _emailRepo.SendToAllAsync(subscribers, blogPost.TitleEN, blogPost.Id);
                return CreatedAtAction(nameof(GetById), new { id = blogPost.Id }, blogPost.ToBlogPostDto());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [TokenAuth]
        public async Task<IActionResult> Edit(EditBlogPostDto editBlogPostDto)
        {
            try
            {
                // find trail, or null if doesn't exist
                var trail = await _context.Trails.FindAsync(editBlogPostDto.TrailId);

                var editedPost = await _blogPostRepo.EditAsync(trail, editBlogPostDto);

                if (editedPost == null)
                {
                    return NotFound();
                }
                return Ok(editedPost.ToBlogPostDto());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("update-view-count/{id:int}")]
        public async Task<IActionResult> UpdateViewCount(int id)
        {
            var post = await _blogPostRepo.UpdateViewCountAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [TokenAuth]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                // this returns the post
                var post = await _blogPostRepo.DeleteAsync(id);

                if (post == null)
                {
                    return NotFound();
                }

                // this deletes the image from s3
                await _imageRepo.DeleteImageFromS3Async(post.CoverImageUrl);

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }


}