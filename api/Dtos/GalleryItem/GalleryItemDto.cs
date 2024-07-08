using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.GalleryItem
{
    public class GalleryItemDto
    {
        public int Id { get; set; }
        // url for photo
        public string Url { get; set; } = string.Empty;
    }
}