using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.GalleryItem;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly ApplicationDBContext _context;

        public GalleryRepository(
            ApplicationDBContext context
        )
        {
            _context = context;
        }
        public async Task<GalleryItem> CreateAsync(GalleryItem galleryItem)
        {
            await _context.GalleryItems.AddAsync(galleryItem);
            await _context.SaveChangesAsync();
            return galleryItem;
        }

        public async Task<GalleryItem?> DeleteAsync(int id)
        {
            var image = await _context.GalleryItems.FirstOrDefaultAsync(x => x.Id == id);

            if (image == null)
            {
                return null;
            }

            _context.GalleryItems.Remove(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<List<GalleryItem>> GetAllAsync(QueryObject queryObject)
        {
            var images = _context.GalleryItems.AsQueryable();

            // pagination
            var skipAmount = (queryObject.PageNumber - 1) * queryObject.PageSize;
            images = images.Skip(skipAmount).Take(queryObject.PageSize);

            return await images.ToListAsync();
        }
    }
}