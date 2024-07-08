using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class S3object
    {
        public string? Name { get; set; }
        public MemoryStream? InputStream { get; set; }
        public string? BucketName { get; set; }
    }
}