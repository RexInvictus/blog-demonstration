using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.BlogPost;
using api.Models;

namespace api.Mappers
{
    public static class BlogPostMappers
    {
        public static BlogPostDto ToBlogPostDto(this BlogPost blogPost)
        {
            return new BlogPostDto
            {
                Id = blogPost.Id,
                ViewCount = blogPost.ViewCount,
                TitleEN = blogPost.TitleEN,
                TitleLTU = blogPost.TitleLTU,
                SubtitleEN = blogPost.SubtitleEN,
                SubtitleLTU = blogPost.SubtitleLTU,
                CoverImageUrl = blogPost.CoverImageUrl,
                ContentEN = blogPost.ContentEN,
                ContentLTU = blogPost.ContentLTU,
                DatePosted = blogPost.DatePosted,
                Comments = blogPost.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static BlogPostPreviewDto ToBlogPostPreviewDto(this BlogPost blogPost)
        {
            return new BlogPostPreviewDto
            {
                Id = blogPost.Id,
                ViewCount = blogPost.ViewCount,
                TitleEN = blogPost.TitleEN,
                SubtitleEN = blogPost.SubtitleEN,
                CoverImageUrl = blogPost.CoverImageUrl,
                DatePosted = blogPost.DatePosted
            };
        }

        public static BlogPost FromCreateToBlogPost(this CreateBlogPostDto createBlogPostDto, Trail? trail)
        {
            return new BlogPost
            {
                TitleEN = createBlogPostDto.TitleEN,
                TitleLTU = createBlogPostDto.TitleLTU,
                SubtitleEN = createBlogPostDto.SubtitleEN,
                SubtitleLTU = createBlogPostDto.SubtitleLTU,
                CoverImageUrl = createBlogPostDto.CoverImageUrl,
                ContentEN = createBlogPostDto.ContentEN,
                ContentLTU = createBlogPostDto.ContentLTU,
                DatePosted = DateTime.UtcNow,
                Trail = trail
            };
        }
    }
}