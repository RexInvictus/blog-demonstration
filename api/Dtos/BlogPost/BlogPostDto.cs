using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Dtos.BlogPost
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        // number of views
        public int ViewCount { get; set; }
        // title
        public string TitleEN { get; set; } = string.Empty;
        public string TitleLTU { get; set; } = string.Empty;
        // subtitle
        public string? SubtitleEN { get; set; }
        public string? SubtitleLTU { get; set; }
        // cover image
        public string? CoverImageUrl { get; set; }
        // content (html format from Quill editor) English version
        public string ContentEN { get; set; } = string.Empty;
        // content LTU version
        public string ContentLTU { get; set; } = string.Empty;
        // date it was posted
        public DateTime DatePosted { get; set; }
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
        // associated trail (to implement later)
    }
}