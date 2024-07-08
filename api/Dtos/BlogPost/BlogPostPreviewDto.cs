using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.BlogPost
{
    public class BlogPostPreviewDto
    {
        public int Id { get; set; }
        // number of views
        public int ViewCount { get; set; }
        // title
        public string TitleEN { get; set; } = string.Empty;
        // subtitle
        public string? SubtitleEN { get; set; }
        // cover image
        public string? CoverImageUrl { get; set; }
        // content (html format from Quill editor) English version
        public DateTime DatePosted { get; set; }
        // associated trail (to implement later)
    }
}