using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.GalleryItem;
using api.Models;

namespace api.Mappers
{
    public static class GalleryItemMappers
    {
        public static GalleryItemDto ToGalleryItemDto(this GalleryItem galleryItem)
        {
            return new GalleryItemDto
            {
                Id = galleryItem.Id,
                Url = galleryItem.Url
            };
        }

        public static GalleryItem FromCreateToGalleryItem(this CreateGalleryItemDto createGalleryItemDto, string? url)
        {
            if (url == null)
            {
                throw new Exception();
            }
            return new GalleryItem
            {
                Url = url
            };
        }
    }
}