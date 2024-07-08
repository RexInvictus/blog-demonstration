using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Trail;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/trail")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trailRepo;
        private readonly ApplicationDBContext _context;
        private readonly IImageRepository _imageRepo;

        public TrailController(ITrailRepository trailRepo, ApplicationDBContext context, IImageRepository imageRepo)
        {
            _trailRepo = trailRepo;
            _context = context;
            _imageRepo = imageRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var posts = await _trailRepo.GetAllAsync(query);
            var trailDtos = posts.Select(t => t.ToTrailDto()).ToList();
            return Ok(trailDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var blogPosts = await _trailRepo.GetByIdAsync(id);
            var blogPostPreviewDtos = blogPosts.Select(p => p.ToBlogPostPreviewDto()).ToList();
            return Ok(blogPostPreviewDtos);
        }

        [HttpPost]
        [TokenAuth]
        public async Task<IActionResult> Create(CreateTrailDto createTrailDto)
        {
            try
            {
                var trail = createTrailDto.FromCreateToTrail();
                // save to db
                await _trailRepo.CreateAsync(trail);
                // return the trail with ok message
                return Ok(trail);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [TokenAuth]
        public async Task<IActionResult> Edit(EditTrailDto editTrailDto)
        {
            try
            {
                // edits, skips image if url is null, returns trail
                var trail = await _trailRepo.EditAsync(editTrailDto);

                if (trail == null)
                {
                    return NotFound();
                }

                return Ok(trail);

            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [TokenAuth]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var trail = await _trailRepo.DeleteAsync(id);

                if (trail == null)
                {
                    return NotFound();
                }

                await _imageRepo.DeleteImageFromS3Async(trail.CoverImageUrl);

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }


    }
}