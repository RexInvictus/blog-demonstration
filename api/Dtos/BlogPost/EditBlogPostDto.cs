using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.BlogPost
{
    public class EditBlogPostDto
    {
        public int Id { get; set; }
        // title
        public string TitleEN { get; set; } = string.Empty;
        public string TitleLTU { get; set;} = string.Empty;
        // subtitle (possibly null)
        public string? SubtitleEN { get; set; }
        public string? SubtitleLTU { get; set; }
        // cover image
        public string? CoverImageUrl { get; set; }
        // content (html format from Quill editor) English version
        public string ContentEN { get; set; } = string.Empty;
        // content LTU version
        public string ContentLTU { get; set; } = string.Empty;
        // associated trail id (possibly null)
        public int TrailId { get; set; } = 0;
    }
}