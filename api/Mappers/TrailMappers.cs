using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Trail;
using api.Models;

namespace api.Mappers
{
    public static class TrailMappers
    {
        public static TrailDto ToTrailDto(this Trail trail)
        {
            return new TrailDto
            {
                Id = trail.Id,
                Name = trail.Name,
                CoverImageUrl = trail.CoverImageUrl,
                Km = trail.Km,
                Days = trail.Days,
                Status = (Dtos.Trail.TrailStatus)trail.Status,
                DateStarted = trail.DateStarted,
                DateEnded = trail.DateEnded,
            };
        }

        public static Trail FromCreateToTrail(this CreateTrailDto createTrailDto)
        {
            return new Trail
            {
                Name = createTrailDto.Name,
                CoverImageUrl = createTrailDto.CoverImageUrl,
                Km = createTrailDto.Km,
                Days = createTrailDto.Days,
                Status = (Models.TrailStatus)createTrailDto.Status,
                DateStarted = createTrailDto.DateStarted,
                DateEnded = createTrailDto.DateEnded
            };
        }
    }
}