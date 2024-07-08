using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Trail;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IImageRepository _imageRepo;
        private readonly IBlogPostRepository _blogPostRepo;

        public TrailRepository(ApplicationDBContext context, IImageRepository imageRepo, IBlogPostRepository blogPostRepo)
        {
            _context = context;
            _imageRepo = imageRepo;
            _blogPostRepo = blogPostRepo;
        }

        public async Task<Trail> CreateAsync(Trail trail)
        {
            await _context.Trails.AddAsync(trail);
            await _context.SaveChangesAsync();
            return trail;
        }

        public async Task<Trail?> DeleteAsync(int id)
        {
            // get blogposts associated with trail
            var posts = await GetByIdAsync(id);
            // update their associated trails to null
            foreach (BlogPost blogPost in posts)
            {
                blogPost.Trail = null;
            }


            var trail = await _context.Trails.FirstOrDefaultAsync(x => x.Id == id);

            if (trail == null)
            {
                return null;
            }

            _context.Trails.Remove(trail);
            await _context.SaveChangesAsync();
            return trail;
        }

        public async Task<Trail?> EditAsync(EditTrailDto editTrailDto)
        {
            var existingTrail = await _context.Trails.FirstOrDefaultAsync(p => p.Id == editTrailDto.Id);

            if (existingTrail == null)
            {
                return null;
            }

            existingTrail.Name = editTrailDto.Name;
            if (editTrailDto.CoverImageUrl != null)
            {
                existingTrail.CoverImageUrl = editTrailDto.CoverImageUrl;
            }
            existingTrail.Km = editTrailDto.Km;
            existingTrail.Days = editTrailDto.Days;
            existingTrail.Status = (Models.TrailStatus)editTrailDto.Status;
            existingTrail.DateStarted = editTrailDto.DateStarted;
            existingTrail.DateEnded = editTrailDto.DateEnded;

            await _context.SaveChangesAsync();

            return existingTrail;
        }

        public async Task<List<Trail>> GetAllAsync(QueryObject query)
        {
            var trails = _context.Trails.AsQueryable();

            if (query.OrderByMostRecent)
            {
                trails = trails.OrderByDescending(t => t.DateStarted);
            }
            else
            {
                trails = trails.OrderBy(t => t.DateStarted);
            }

            // pagination
            var skipAmount = (query.PageNumber - 1) * query.PageSize;
            trails = trails.Skip(skipAmount).Take(query.PageSize);

            return await trails.ToListAsync();
        }

        public async Task<List<BlogPost>> GetByIdAsync(int id)
        {
            var query = new QueryObject { TrailId = id };
            var blogPosts = await _blogPostRepo.GetAllAsync(query);
            return blogPosts;
        }
    }
}