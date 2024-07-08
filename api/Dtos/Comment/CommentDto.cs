using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        // name of commentor
        public string Name { get; set; } = string.Empty;
        // content
        public string Content { get; set; } = string.Empty;
    }
}