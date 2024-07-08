using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        // associated post ID
        public int BlogPostId { get; set; }
        // associated BlogPost
        public BlogPost? Post { get; set; }
        // name of commentor
        public string Name { get; set; } = string.Empty;
        // email of commentor
        public string Email { get; set; } = string.Empty;
        // content
        public string Content { get; set; } = string.Empty;
    }
}