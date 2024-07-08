using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Name = comment.Name,
                Content = comment.Content
            };
        }

        public static Comment FromCreateToComment(this CreateCommentDto createCommentDto)
        {
            return new Comment
            {
                Name = createCommentDto.Name,
                Email = createCommentDto.Email,
                Content = createCommentDto.Content,
                BlogPostId = createCommentDto.BlogPostId
            };
        }
    }
}