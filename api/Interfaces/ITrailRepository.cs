using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Trail;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ITrailRepository
    {
        Task<List<Trail>> GetAllAsync(QueryObject query);
        Task<Trail> CreateAsync(Trail trail);
        Task<Trail?> EditAsync(EditTrailDto editTrailDto);
        Task<Trail?> DeleteAsync(int id);
        Task<List<BlogPost>> GetByIdAsync(int id);
    }
}