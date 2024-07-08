using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.GalleryItem
{
    public class CreateGalleryItemDto
    {
        [Required]
        public IFormFile? Image { get; set; } = null;
    }
}