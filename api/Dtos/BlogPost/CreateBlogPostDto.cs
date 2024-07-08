using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.BlogPost
{
    public class CreateBlogPostDto
    {
        // title
        public string TitleEN { get; set; } = string.Empty;
        public string TitleLTU { get; set; } = string.Empty;
        // subtitle
        public string? SubtitleEN { get; set; }
        public string? SubtitleLTU { get; set; }
        // cover image url
        public string? CoverImageUrl { get; set; }
        // content (html format from Quill editor) English version
        public string ContentEN { get; set; } = string.Empty;
        // content LTU version
        public string ContentLTU { get; set; } = string.Empty;
        // date it was posted
        public int TrailId { get; set; } = 0;
    }
}