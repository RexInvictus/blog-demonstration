using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.GalleryItem;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IGalleryRepository
    {
        Task<List<GalleryItem>> GetAllAsync(QueryObject queryObject);
        Task<GalleryItem> CreateAsync(GalleryItem galleryItem);
        Task<GalleryItem?> DeleteAsync(int id);
    }
}