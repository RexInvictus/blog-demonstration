using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Trail
{
    public class EditTrailDto
    {
        public int Id { get; set; }
        // name of the trail
        public string Name { get; set; } = string.Empty;
        // generally a map photo
        public string? CoverImageUrl { get; set; }
        // how many kilometres it is
        public int Km { get; set; }
        // how many days it lasts (approx if ongoing, later updated to exact)
        public int Days { get; set; }
        // status of the trail
        public TrailStatus Status { get; set; } = TrailStatus.Planned;
        // date the trail started (null if planned)
        public DateTime? DateStarted { get; set; }
        // date the trail ended (null if ongoing)
        public DateTime? DateEnded { get; set; }
    }
}