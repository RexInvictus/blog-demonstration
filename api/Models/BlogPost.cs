using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        // number of views
        public int ViewCount { get; set; } = 0;
        // title English
        public string TitleEN { get; set; } = string.Empty;
        // title LTU
        public string TitleLTU { get; set; } = string.Empty;
        // subtitle (possibly null) English
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
        // list of associated comments 
        public List<Comment> Comments { get; set; } = new List<Comment>();
        // associated trail id (possibly null)
        public Trail? Trail { get; set; }
    }
}