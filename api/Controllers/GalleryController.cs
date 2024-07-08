using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.GalleryItem;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/gallery")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IImageRepository _imageRepo;
        private readonly IGalleryRepository _galleryRepo;

        public GalleryController(
            ApplicationDBContext context,
            IImageRepository imageRepo,
            IGalleryRepository galleryRepo
        )
        {
            _context = context;
            _imageRepo = imageRepo;
            _galleryRepo = galleryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var images = await _galleryRepo.GetAllAsync(query);
            var galleryItemDtos = images.Select(p => p.ToGalleryItemDto()).ToList();
            return Ok(galleryItemDtos);
        }

        [HttpPost]
        [TokenAuth]
        public async Task<IActionResult> Create(CreateGalleryItemDto createGalleryItemDto)
        {
            try
            {
                
                var url = await _imageRepo.CreateAsync(createGalleryItemDto.Image);
                var galleryItem = createGalleryItemDto.FromCreateToGalleryItem(url);
                await _galleryRepo.CreateAsync(galleryItem);
                return Ok(galleryItem);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [TokenAuth]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var image = await _galleryRepo.DeleteAsync(id);

                if (image == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}