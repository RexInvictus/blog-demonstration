using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class GalleryItem
    {
        public int Id { get; set; }
        // url for photo
        public string Url { get; set; } = string.Empty;

        internal object ToGalleryDto()
        {
            throw new NotImplementedException();
        }
    }
}