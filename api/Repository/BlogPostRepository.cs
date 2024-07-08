using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.BlogPost;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IImageRepository _imageRepo;

        public BlogPostRepository(ApplicationDBContext context, IImageRepository imageRepo)
        {
            _context = context;
            _imageRepo = imageRepo;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(int id)
        {
            var post = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
            {
                return null;
            }

            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost?> EditAsync(Trail? trail, EditBlogPostDto editBlogPostDto)
        {
            var existingPost = await _context.BlogPosts.FirstOrDefaultAsync(p => p.Id == editBlogPostDto.Id);

            if (existingPost == null)
            {
                return null;
            }

            existingPost.TitleEN = editBlogPostDto.TitleEN;
            existingPost.TitleLTU = editBlogPostDto.TitleLTU;
            existingPost.SubtitleEN = editBlogPostDto.SubtitleEN;
            existingPost.SubtitleLTU = editBlogPostDto.SubtitleLTU;
            existingPost.CoverImageUrl = editBlogPostDto.CoverImageUrl;
            existingPost.ContentEN = editBlogPostDto.ContentEN;
            existingPost.ContentLTU = editBlogPostDto.ContentLTU;
            existingPost.Trail = trail;

            await _context.SaveChangesAsync();

            return existingPost;
        }


        // get blog posts by filter criteria
        public async Task<List<BlogPost>> GetAllAsync(QueryObject query)
        {
            var posts = _context.BlogPosts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                posts = posts.Where(p => p.TitleLTU.Contains(query.Title) || p.TitleEN.Contains(query.Title));
            }

            if (query.TrailId != null)
            {
                posts = posts.Where(p => p.Trail != null && p.Trail.Id == query.TrailId);
            }

            if (query.OrderByMostRecent)
            {
                posts = posts.OrderByDescending(p => p.DatePosted);
            }
            else
            {
                posts = posts.OrderBy(p => p.DatePosted);
            }

            // pagination
            var skipAmount = (query.PageNumber - 1) * query.PageSize;
            posts = posts.Skip(skipAmount).Take(query.PageSize);

            return await posts.ToListAsync();
        }

        // get a full individual blogpost by ID
        public async Task<BlogPost?> GetByIdAsync(int id)
        {
            return await _context.BlogPosts.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<BlogPost?> UpdateViewCountAsync(int id)
        {
            var post = await _context.BlogPosts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return null;
            }

            var newCount = post.ViewCount + 1;
            post.ViewCount = newCount;

            await _context.SaveChangesAsync();
            return post;
        }
    }
}